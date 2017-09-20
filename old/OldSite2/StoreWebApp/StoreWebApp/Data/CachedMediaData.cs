// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="CachedMediaData.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Data
{
    using System.Collections.Concurrent;
    using ShowcaseED.Model;

    /// <summary>
    /// Cached Media Data
    /// </summary>
    public sealed class CachedMediaData
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly CachedMediaData TheInstance = new CachedMediaData();

        /// <summary>
        /// The dump
        /// </summary>
        private static ConcurrentDictionary<int, DumpedMediaFile> dump = new ConcurrentDictionary<int, DumpedMediaFile>();

        /////// <summary>
        /////// Initializes static members of the<see cref="CachedMediaData" /> class.
        /////// </summary>
        ////static CachedMediaData()
        ////{
        ////    ////
        ////}

        /// <summary>
        /// Prevents a default instance of the <see cref="CachedMediaData" /> class from being created.
        /// </summary>
        private CachedMediaData()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static CachedMediaData Instance
        {
            get
            {
                return TheInstance;
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True / False</returns>
        public bool Add(DumpedMediaFile item)
        {
            dump.AddOrUpdate(item.Id, item, (i, file) => item);
            return dump.ContainsKey(item.Id);
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Dumped Media File</returns>
        public DumpedMediaFile Get(int id)
        {
            DumpedMediaFile file;
            if (dump.TryGetValue(id, out file))
            {
                return file;
            }

            return null;
        }
    }
}