// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="ImageThumbnail.ascx.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Controls
{
    using System;

    /// <summary>
    /// Image Thumbnail
    /// </summary>
    public partial class ImageThumbnail : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the file URL fullsize.
        /// </summary>
        /// <value>
        /// The file URL fullsize.
        /// </value>
        public string FileUrlFullsize { get; set; }

        /// <summary>
        /// Gets or sets the file URL preview.
        /// </summary>
        /// <value>
        /// The file URL preview.
        /// </value>
        public string FileUrlPreview { get; set; }

        /// <summary>
        /// Gets or sets the file URL thumbnail.
        /// </summary>
        /// <value>
        /// The file URL thumbnail.
        /// </value>
        public string FileUrlThumbnail { get; set; }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}