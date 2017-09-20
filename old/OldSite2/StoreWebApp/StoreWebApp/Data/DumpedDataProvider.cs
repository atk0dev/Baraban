// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="DumpedDataProvider.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using NLog;
    
    using ShowcaseED.Interfaces;
    
    using ShowcaseED.Model;

    /// <summary>
    /// Dumped Data Provider
    /// </summary>
    public class DumpedDataProvider
    {
        /// <summary>
        /// The logger
        /// </summary>
        private Logger logger;

        /// <summary>
        /// The temp folder
        /// </summary>
        private string tempFolder;

        /// <summary>
        /// The cached data
        /// </summary>
        private CachedMediaData cachedData;

        /// <summary>
        /// The connection string
        /// </summary>
        private string connectionString;

        /// <summary>
        /// The thumbnail literal
        /// </summary>
        private string thumbnailLiteral = "thumbnail";

        /// <summary>
        /// The preview literal
        /// </summary>
        private string previewLiteral = "preview";

        /// <summary>
        /// The fullsize literal
        /// </summary>
        private string fullsizeLiteral = "fullsize";

        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedDataProvider" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="tmpFolder">The TMP folder.</param>
        public DumpedDataProvider(string connectionString, string tmpFolder)
        {
            this.cachedData = CachedMediaData.Instance;
            this.tempFolder = tmpFolder;

            this.logger = LogManager.GetCurrentClassLogger();
            this.connectionString = connectionString;
            this.WriteMessage(string.Format("DumpedDataProvider created with connection string: [{0}]", this.connectionString));
        }

        /// <summary>
        /// Loads the media.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="from">Image From.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// List of DumpedMediaFiles
        /// </returns>
        /// <exception cref="System.Exception">Unable to find TempWebFolder setting.</exception>
        public IEnumerable<DumpedMediaFile> LoadMedia(int groupId, int from, int count)
        {
            if (string.IsNullOrEmpty(this.tempFolder))
            {
                throw new Exception("Unable to find TempWebFolder setting.");
            }

            var dumpedList = new List<DumpedMediaFile>();

            IStoreDataSource ds = new StoreDataProvider(this.connectionString);
            List<MediaDraft> list;
            if (groupId == 0)
            {
                list = ds.LoadAllMedia().ToList();
            }
            else
            {
                list = ds.LoadPartMedia(groupId, from, count).ToList();
            }

            var threadList = new List<Thread>();

            foreach (var item in list)
            {
                var cachedItem = this.ReadItemFromCache(item.Id);
                if (cachedItem != null)
                {
                    this.logger.Info(string.Format("Reading image from cache. Image Id = {0}", item.Id));
                    dumpedList.Add(cachedItem);
                }
                else
                {
                    this.logger.Info(string.Format("Dumping image and putting to cache. Image Id = {0}", item.Id));

                    if (item.ServerFilePath != null)
                    {
                        string guid = Guid.NewGuid().ToString();
                        string localFileName = Path.GetFileName(item.ServerFilePath).Replace(" ", string.Empty);

                        string currentFolder = Path.Combine(this.tempFolder, guid);
                        Directory.CreateDirectory(currentFolder);

                        var di = new DumpedMediaFile();
                        di.Thumbnail.LocalFilePath = Path.Combine(
                            currentFolder, string.Format("{0}_{1}_{2}", item.Id, this.thumbnailLiteral, localFileName));
                        di.Preview.LocalFilePath = Path.Combine(
                            currentFolder, string.Format("{0}_{1}_{2}", item.Id, this.previewLiteral, localFileName));
                        di.Fullsize.LocalFilePath = Path.Combine(
                            currentFolder, string.Format("{0}_{1}_{2}", item.Id, this.fullsizeLiteral, localFileName));

                        di.Id = item.Id;
                        di.GroupId = item.GroupId;

                        threadList.Add(this.SaveFileToLocalTemp(di, item.Media, item.FileSize));

                        di.FileDateTime = item.FileDateTime;

                        di.Thumbnail.GlobalFileUrl = string.Format(
                            @"/temp/{0}/{1}",
                            guid,
                            string.Format("{0}_{1}_{2}", item.Id, this.thumbnailLiteral, localFileName));
                        di.Preview.GlobalFileUrl = string.Format(
                            @"temp/{0}/{1}",
                            guid,
                            string.Format("{0}_{1}_{2}", item.Id, this.previewLiteral, localFileName));
                        di.Fullsize.GlobalFileUrl = string.Format(
                            @"temp/{0}/{1}",
                            guid,
                            string.Format("{0}_{1}_{2}", item.Id, this.fullsizeLiteral, localFileName));

                        this.AddItemToCache(di);

                        dumpedList.Add(di);
                    }
                }
            }

            foreach (var thread in threadList)
            {
                thread.Join();
            }

            return dumpedList;
        }

        /// <summary>
        /// Gets the images count.
        /// </summary>
        /// <returns>Images count</returns>
        public int GetImagesCount()
        {
            IStoreDataSource ds = new StoreDataProvider(this.connectionString);
            return ds.GetImagesCount();
        }

        /// <summary>
        /// Reads the item from cache.
        /// </summary>
        /// <param name="dumpedMediaId">The dumped media id.</param>
        /// <returns>Dumped Media File</returns>
        private DumpedMediaFile ReadItemFromCache(int dumpedMediaId)
        {
            return this.cachedData.Get(dumpedMediaId);
        }

        /// <summary>
        /// Adds the item to cache.
        /// </summary>
        /// <param name="item">The item.</param>
        private void AddItemToCache(DumpedMediaFile item)
        {
            this.cachedData.Add(item);
        }

        /// <summary>
        /// Saves the file to local temp.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="data">The data.</param>
        /// <param name="size">The size.</param>
        /// <returns>Dump file thread</returns>
        private Thread SaveFileToLocalTemp(DumpedMediaFile file, byte[] data, int size)
        {
            FileDumper fileDumper = new FileDumper() { File = file, Data = data, Size = size };
            Thread dumpThread = new Thread(new ThreadStart(fileDumper.Dump));
            dumpThread.Start();
            return dumpThread;
        }

        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void WriteMessage(string msg)
        {
            this.logger.Info(msg);
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void WriteError(string msg)
        {
            this.logger.Error(msg);
        }
    }
}