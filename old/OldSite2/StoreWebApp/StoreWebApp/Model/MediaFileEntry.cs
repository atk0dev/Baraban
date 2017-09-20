// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="MediaFileEntry.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Model
{
    /// <summary>
    /// Media File Entry
    /// </summary>
    public class MediaFileEntry
    {
        /// <summary>
        /// Gets or sets the local file path.
        /// </summary>
        /// <value>
        /// The local file path.
        /// </value>
        public string LocalFilePath { get; set; }

        /// <summary>
        /// Gets or sets the global file URL.
        /// </summary>
        /// <value>
        /// The global file URL.
        /// </value>
        public string GlobalFileUrl { get; set; }
    }
}