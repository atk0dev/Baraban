// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="Edit.aspx.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED
{
    using System;

    /// <summary>
    /// Edit page code 
    /// </summary>
    public partial class Edit : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Session["AdminUser"] == null)
            {
                this.MyImageGrid.Visible = false;
                this.Login.Visible = true;
            }
            else
            {
                this.MyImageGrid.Visible = true;
                this.Login.Visible = false;
            }
        }
    }
}