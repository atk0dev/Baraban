// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="DisplayInfo.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED
{
    /// <summary>
    /// Display Info
    /// </summary>
    internal class DisplayInfo
    {
        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>
        /// The group id.
        /// </value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// Image From.
        /// </value>
        public int From { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the images count.
        /// </summary>
        /// <value>
        /// The images count.
        /// </value>
        public int ImagesCount { get; set; }

        /// <summary>
        /// Gets or sets the conn STR.
        /// </summary>
        /// <value>
        /// The conn STR.
        /// </value>
        public string ConnStr { get; set; }
    }
}
