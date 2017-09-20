// ***********************************************************************
// Assembly         : WinKAS.WebApi
// Author           : Andrii Tkach
// Created          : 07-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 07-12-2012
// ***********************************************************************
// <copyright file="PublicErrorHelper.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    Return error message by error code
//
// 10000 - 19999 General Errors
// 20000 - 29999 Security Errors
// 30000 - 39999 General WinKAS errors
// 40000 - 49999 Financial System errors
// 50000 - 59999 Stock system errors
//
// </summary>
// ***********************************************************************

namespace ShowcaseED.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// WinKAS Error Helper
    /// </summary>
    public static class PublicErrorHelper
    {
        /// <summary>
        /// The errors
        /// </summary>
        private static readonly Dictionary<int, string> Errors = new Dictionary<int, string>()
            {
                { 10100, "Request is empty" },
                { 10101, "WinKAS team did not implement this feature yet" },
                { 20010, "Error while validating user token" },
                { 20011, "User token is not valid" },
                { 20012, "Error while authentication user" },
                { 20013, "Unable to authenticate user" },
                { 20014, "User token is empty" },
                { 20110, "WinKAS users list was not found" },
                { 20111, "Error while getting WinKAS users list" },
                { 20112, "WinKAS user was not found" },
                { 20113, "Error while creating new WinKAS user" },
                { 20114, "Error while updating WinKAS user" },
                { 20115, "Error while deleting WinKAS user" },
                { 30101, "Error while uploading media" },
                { 30102, "Media request is not well formatted" },
                { 30103, "Error while processing media request data" },
                { 30104, "No token found in the media form data" },
                { 30105, "Media request was Faulted or Canceled" },
                { 30106, "Media data was not saved in the WinKAS system" },
                { 30201, "Products was not found in the WinKAS system" },
                { 30202, "Error occurred while getting WinKAS products" },
                { 30203, "Product category does not exists in the WinKAS system" },
                { 40100, "There are no accounts in the WinKAS system available" },
                { 40101, "Error occurred while getting WinKAS accounts" },
                { 40102, "Error occurred while getting WinKAS accounting" },
                { 40103, "There are no accounting info available in the WinKAS system" }
            };

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>Error message</returns>
        public static string GetError(int errorCode)
        {
            if (Errors.ContainsKey(errorCode))
            {
                return Errors[errorCode];
            }

            return "WinKAS error is not defined.";
        }
    }
}
