// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="DumpedMediaFile.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Model
{
    using System;

    /// <summary>
    /// Dumped Media File
    /// </summary>
    public class DumpedMediaFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DumpedMediaFile" /> class.
        /// </summary>
        public DumpedMediaFile()
        {
            this.Thumbnail = new MediaFileEntry();
            this.Preview = new MediaFileEntry();
            this.Fullsize = new MediaFileEntry();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>
        /// The thumbnail.
        /// </value>
        public MediaFileEntry Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the preview.
        /// </summary>
        /// <value>
        /// The preview.
        /// </value>
        public MediaFileEntry Preview { get; set; }

        /// <summary>
        /// Gets or sets the fullsize.
        /// </summary>
        /// <value>
        /// The fullsize.
        /// </value>
        public MediaFileEntry Fullsize { get; set; }

        /// <summary>
        /// Gets or sets the file date time.
        /// </summary>
        /// <value>
        /// The file date time.
        /// </value>
        public DateTime FileDateTime { get; set; }

        /// <summary>
        /// Gets or sets the uploaded token.
        /// </summary>
        /// <value>
        /// The uploaded token.
        /// </value>
        public string UploadedToken { get; set; }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>
        /// The group id.
        /// </value>
        public int GroupId { get; set; }
    }
}