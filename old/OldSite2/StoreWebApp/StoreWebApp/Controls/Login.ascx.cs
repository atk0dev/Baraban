// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-07-2013
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="Login.ascx.cs" company="WinKAS A/S">
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
    /// Login page
    /// </summary>
    public partial class Login : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (this.tbLogin.Text.Equals("mama", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Session["AdminUser"] = true;
            }
            else
            {
                this.Session["AdminUser"] = null;
            }
        }
    }
}