// ***********************************************************************
// Assembly         : WinKAS.WebApi
// Author           : Andrii Tkach
// Created          : 03-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 03-12-2012
// ***********************************************************************
// <copyright file="EditImage.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    WinKAS data provider for getting data from old MySQL databases.
// </summary>
// ***********************************************************************

namespace ShowcaseED.Model
{
    /// <summary>
    /// Image for Edit page
    /// </summary>
    public class EditImage
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>
        /// The group.
        /// </value>
        public int GroupId { get; set; }
    }
}