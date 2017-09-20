// ***********************************************************************
// Assembly         : WinKAS.Domain
// Author           : Michael Jørgensen
// Created          : 24-11-2012
//
// Last Modified By : Michael Jørgensen
// ***********************************************************************
// <copyright file="Mediadraft.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    This poco class is for mediadata received througt the API. Mediadata can be images - Sounds etc.
//    The class defines a draft to hold the data temporary.
// </summary>
// ************************************************************************

namespace ShowcaseED.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Class MediaDraft defines the poco class for the table "Mediadraft".
    /// </summary>
    public class MediaDraft
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a Unique Id for the MediaDraft
        /// </summary>
        [Key]
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>
        /// The group id.
        /// </value>
        public virtual int GroupId { get; set; }

        /// <summary>
        /// Gets or sets Media. Holds the streamed value of the media.
        /// </summary>
        public virtual byte[] Media { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public virtual int FileSize { get; set; }

        /// <summary>
        /// Gets or sets the file date time.
        /// </summary>
        /// <value>
        /// The file date time.
        /// </value>
        public virtual DateTime FileDateTime { get; set; }
        
        /// <summary>
        /// Gets or sets the server path.
        /// </summary>
        /// <value>
        /// The server path.
        /// </value>
        public virtual string ServerFilePath { get; set; }

        #endregion
    }
}
