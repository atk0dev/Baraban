// ***********************************************************************
// Assembly         : WinKAS.Authentication
// Author           : Andrii Tkach
// Created          : 11-07-2013
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="ProductGroup.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    Product Group
// </summary>
// ***********************************************************************

namespace ShowcaseED.Model
{
    /// <summary>
    /// Product Group
    /// </summary>
    public class ProductGroup
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}