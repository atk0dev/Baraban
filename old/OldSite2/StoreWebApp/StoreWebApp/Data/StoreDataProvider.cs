// ***********************************************************************
// Assembly         : WinKAS.WebApi
// Author           : Andrii Tkach
// Created          : 03-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 03-12-2012
// ***********************************************************************
// <copyright file="StoreDataProvider.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    WinKAS data provider for getting data from old MySQL databases.
// </summary>
// ***********************************************************************

namespace ShowcaseED.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using NLog;

    using ShowcaseED.Interfaces;

    using ShowcaseED.Model;
    using ShowcaseED.Utils;

    /// <summary>
    /// Old Data Provider
    /// </summary>
    public class StoreDataProvider : IStoreDataSource
    {
        /// <summary>
        /// The connection
        /// </summary>
        private readonly string connStr;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreDataProvider" /> class.
        /// </summary>
        /// <param name="databaseConnection">The database connection.</param>
        public StoreDataProvider(string databaseConnection)
        {
            this.connStr = databaseConnection;
            this.logger.Info(string.Format("Connection string for old data provider used for WinKAS database: {0}", databaseConnection));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StoreDataProvider" /> class from being created.
        /// </summary>
        private StoreDataProvider()
        {
        }

        /// <summary>
        /// Loads the media file.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// MediaDraft entity
        /// </returns>
        public MediaDraft LoadMediaFile(int id)
        {
            this.logger.Info("Loading single Media file from old MySQL database...");
            try
            {
                MediaDraft media = null;

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("LoadSingleMedia");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            if (data.Read())
                            {
                                media = this.BuildMediaDraftFromReader(data);
                            }
                            else
                            {
                                this.logger.Info(string.Format("Image not found using id={0}", id));
                            }
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded single image from the database."));

                return media;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while loading single image. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Loading single Media file from old MySQL database completed.");
            }
        }

        /// <summary>
        /// Loads all media.
        /// </summary>
        /// <returns>
        /// Ordered by Date List of all media
        /// </returns>
        public IEnumerable<MediaDraft> LoadAllMedia()
        {
            this.logger.Info("Loading data from old MySQL database...");
            try
            {
                var list = new List<MediaDraft>();

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("LoadAllMedia");
                        
                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                list.Add(this.BuildMediaDraftFromReader(data));
                            }
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded images from the database. Count: {0}", list.Count));

                return list;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while loading image. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Loading data from old MySQL database completed.");
            }
        }

        /// <summary>
        /// Loads the part media.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="from">From row.</param>
        /// <param name="count">To row count.</param>
        /// <returns>
        /// Media data
        /// </returns>
        public IEnumerable<MediaDraft> LoadPartMedia(int groupId, int from, int count)
        {
            this.logger.Info("Loading data from old MySQL database...");
            try
            {
                if ((from < 0) || (from > count + from))
                {
                    from = 0;
                }

                if (count == 0)
                {
                    count = int.MaxValue;
                }

                var list = new List<MediaDraft>();

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("LoadPartMedia");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@GroupId", groupId));
                        cmd.Parameters.Add(new SqlParameter("@From", from));
                        cmd.Parameters.Add(new SqlParameter("@Count", count));

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                list.Add(this.BuildMediaDraftFromReader(data));
                            }
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded images from the database. Count: {0}", list.Count));

                return list;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while loading image. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Loading data from old MySQL database completed.");
            }
        }

        /// <summary>
        /// Gets the images count.
        /// </summary>
        /// <returns>
        /// WinKAS Uploaded images amount.
        /// </returns>
        public int GetImagesCount()
        {
            this.logger.Info("Getting images count from old MySQL database...");
            try
            {
                int imgCount = 0;

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("select count(Id) from MediaDrafts");
                        cmd.CommandText = commandBuilder.ToString();

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            data.Read();
                            imgCount = data.GetInt32(0);
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded images amount from the database. Count: {0}", imgCount));

                return imgCount;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while getting images amount. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Getting images count from old MySQL database completed.");
            }
        }

        /// <summary>
        /// Loads the catalog.
        /// </summary>
        /// <returns>
        /// List of catalog items
        /// </returns>
        public IEnumerable<CatalogItem> LoadCatalog()
        {
            this.logger.Info("Loading data from old MySQL database...");
            try
            {
                var list = new List<CatalogItem>();

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("LoadCatalog");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                var item = new CatalogItem();
                                item.Id = Convert.ToInt32(data["Id"]);
                                item.Caption = Convert.ToString(data["Caption"]);
                                item.Url = Convert.ToString(data["Url"]);
                                list.Add(item);
                            }
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded images from the database. Count: {0}", list.Count));

                return list;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while loading image. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Loading data from old MySQL database completed.");
            }
        }

        /// <summary>
        /// Saves the media.
        /// </summary>
        /// <param name="draft">The draft.</param>
        /// <returns>
        /// upload result
        /// </returns>
        public bool SaveMedia(MediaDraft draft)
        {
            this.logger.Info("Saving media draft...");
            try
            {
                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("SaveMedia");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Media", draft.Media);
                        cmd.Parameters.AddWithValue("@ServerFilePath", draft.ServerFilePath);
                        cmd.Parameters.AddWithValue("@FileSize", draft.FileSize);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully savedmedia to the database"));

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while saving media. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Saving media draft completed.");
            }
        }

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns>Delete operation result</returns>
        public bool DeleteImage(int imageId)
        {
            this.logger.Info("Deleting media draft...");
            try
            {
                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("DeleteMedia");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", imageId);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully deleted image"));

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while deleting media. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Delete media draft completed.");
            }
        }

        /// <summary>
        /// Groupses this instance.
        /// </summary>
        /// <returns>
        /// List of groups
        /// </returns>
        public IEnumerable<ProductGroup> Groups()
        {
            this.logger.Info("Loading Catalog...");
            try
            {
                var list = new List<ProductGroup>();

                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("LoadCatalog");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                list.Add(this.BuildProductGroupFromReader(data));
                            }
                        }
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully loaded catalog. Count: {0}", list.Count));

                return list;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while loading catalog. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Loading catalog completed.");
            }
        }

        /// <summary>
        /// Updates the media.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Updates existing media item
        /// </returns>
        public bool UpdateMedia(MediaDraft item)
        {
            this.logger.Info("Updating media draft...");
            try
            {
                using (var conn = new SqlConnection(this.connStr))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        var commandBuilder = new StringBuilder();
                        commandBuilder.AppendFormat("UpdateMedia");

                        cmd.CommandText = commandBuilder.ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@GroupId", item.GroupId);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                this.logger.Info(string.Format("Successfully updated image"));

                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while updating media. Message: {0}", ex.Message));
                throw;
            }
            finally
            {
                this.logger.Info("Update media draft completed.");
            }
        }

        /// <summary>
        /// Logs the object.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="obj">The object.</param>
        private void LogObject(string message, object obj)
        {
            string fullString = ObjectDumper.WriteAsText(obj);
            this.logger.Info(string.Format("{0} {1}", message, fullString));
        }

        /// <summary>
        /// Builds the media draft from reader.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>MediaDraft entity</returns>
        private MediaDraft BuildMediaDraftFromReader(SqlDataReader data)
        {
            var item = new MediaDraft();

            try
            {
                item.Id = Convert.ToInt32(data["Id"]);

                item.GroupId = data["GroupId"] is DBNull ? 0 : Convert.ToInt32(data["GroupId"]);

                item.FileSize = data["FileSize"] is DBNull ? 0 : Convert.ToInt32(data["FileSize"]);

                item.FileDateTime = data["FileDateTime"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(data["FileDateTime"]);

                item.ServerFilePath = data["ServerFilePath"] is DBNull ? string.Empty : Convert.ToString(data["ServerFilePath"]);

                item.Media = (byte[])data["Media"];
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    string.Format(
                        "Error while reading Media of {0} row. Message: {1}", item.Id, ex.Message));
            }

            return item;
        }

        /// <summary>
        /// Builds the product group from reader.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Products catalog</returns>
        private ProductGroup BuildProductGroupFromReader(SqlDataReader data)
        {
            var item = new ProductGroup();

            try
            {
                item.Id = Convert.ToInt32(data["Id"]);

                item.Name = data["Caption"] is DBNull ? string.Empty : Convert.ToString(data["Caption"]);
            }
            catch (Exception ex)
            {
                this.logger.Error(
                    string.Format(
                        "Error while reading Catalog of {0} row. Message: {1}", item.Id, ex.Message));
            }

            return item;
        }
    }
}
