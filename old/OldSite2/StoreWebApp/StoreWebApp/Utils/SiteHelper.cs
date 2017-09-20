// ***********************************************************************
// Assembly         : WinKAS.Authentication
// Author           : Andrii Tkach
// Created          : 03-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 03-12-2012
// ***********************************************************************
// <copyright file="SiteHelper.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    Site Helper
// </summary>
// ***********************************************************************

namespace ShowcaseED.Utils
{
    using System.IO;
    using System.Web;

    /// <summary>
    /// Site Helper
    /// </summary>
    public static class SiteHelper
    {
        /// <summary>
        /// Temps the file path.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns>
        /// Temp folder
        /// </returns>
        public static string TempFilePath(HttpServerUtility server)
        {
            return Path.Combine(server.MapPath("~"), "temp");
        }
    }
}