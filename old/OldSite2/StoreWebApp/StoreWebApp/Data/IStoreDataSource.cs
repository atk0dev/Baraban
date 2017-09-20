// ***********************************************************************
// Assembly         : WinKAS.Api
// Author           : Andrii Tkach
// Created          : 22-10-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 22-10-2012
// ***********************************************************************
// <copyright file="IStoreDataSource.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    WinKAS Data Storage interface.
// </summary>
// ***********************************************************************

namespace ShowcaseED.Interfaces
{
    using System.Collections.Generic;
    using System.IO;

    using ShowcaseED.Model;

    /// <summary>
    /// WinKAS Data Storage
    /// </summary>
    public interface IStoreDataSource
    {
        /// <summary>
        /// Loads the media file.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>MediaDraft entity</returns>
        MediaDraft LoadMediaFile(int id);

        /// <summary>
        /// Loads all media.
        /// </summary>
        /// <returns>Ordered by Date List of all media</returns>
        IEnumerable<MediaDraft> LoadAllMedia();

        /// <summary>
        /// Loads the part media.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="from">From row.</param>
        /// <param name="count">count rpw.</param>
        /// <returns>
        /// Ordered by Date List of all media range
        /// </returns>
        IEnumerable<MediaDraft> LoadPartMedia(int groupId, int from, int count);

        /// <summary>
        /// Gets the images count.
        /// </summary>
        /// <returns>WinKAS Uploaded images amount.</returns>
        int GetImagesCount();

        /// <summary>
        /// Loads the catalog.
        /// </summary>
        /// <returns>List of catalog items</returns>
        IEnumerable<CatalogItem> LoadCatalog();

        /// <summary>
        /// Saves the media.
        /// </summary>
        /// <param name="draft">The draft.</param>
        /// <returns>upload result</returns>
        bool SaveMedia(MediaDraft draft);

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns>Delete operation result</returns>
        bool DeleteImage(int imageId);

        /// <summary>
        /// Groupses this instance.
        /// </summary>
        /// <returns>List of groups</returns>
        IEnumerable<ProductGroup> Groups();

        /// <summary>
        /// Updates the media.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Updates existing media item</returns>
        bool UpdateMedia(MediaDraft item);
    }
}
