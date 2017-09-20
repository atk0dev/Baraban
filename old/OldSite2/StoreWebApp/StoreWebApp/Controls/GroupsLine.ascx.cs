// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="GroupsLine.ascx.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Controls
{
    using System;
    using System.Web.UI.WebControls;
    using ShowcaseED.Data;
    using ShowcaseED.Utils;

    /// <summary>
    /// Groups Line
    /// </summary>
    public partial class GroupsLine : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets the link prev.
        /// </summary>
        /// <value>
        /// The link prev.
        /// </value>
        public LinkButton LinkPrev 
        { 
            get
            {
                return this.LinkButtonPrev;
            } 
        }

        /// <summary>
        /// Gets the link next.
        /// </summary>
        /// <value>
        /// The link next.
        /// </value>
        public LinkButton LinkNext 
        { 
            get
            {
                return this.LinkButtonNext;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonPrev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void LinkButtonPrev_Click(object sender, EventArgs e)
        {
           var di = Session["DisplayInfo"] as DisplayInfo;
            if (di != null)
            {
                int i = di.From - di.Count;
                di.From = i > 0 ? i : 0;
                var list = new DumpedDataProvider(di.ConnStr, SiteHelper.TempFilePath(Server)).LoadMedia(di.GroupId, di.From, di.Count);
                this.Session["DisplayInfo"] = di;
                this.Session["DumpedMediaList"] = list;

                this.SetControlsState(di.From, di.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void LinkButtonNext_Click(object sender, EventArgs e)
        {
            var di = Session["DisplayInfo"] as DisplayInfo;
            if (di != null)
            {
                di.From = di.From + di.Count;
                var list = new DumpedDataProvider(di.ConnStr, SiteHelper.TempFilePath(Server)).LoadMedia(di.GroupId, di.From, di.Count);
                this.Session["DisplayInfo"] = di;
                this.Session["DumpedMediaList"] = list;

                this.SetControlsState(di.From, di.Count);
            }
        }

        /// <summary>
        /// Sets the state of the controls.
        /// </summary>
        /// <param name="imageFrom">The image from.</param>
        /// <param name="imagesCount">The images count.</param>
        private void SetControlsState(int imageFrom, int imagesCount)
        {
            int max = int.MaxValue;
            var di = Session["DisplayInfo"] as DisplayInfo;
            if ((di != null) && (di.ImagesCount > 0))
            {
                max = di.ImagesCount;
            }

            this.LinkButtonPrev.Visible = imageFrom != 0;
            this.LinkButtonNext.Visible = imageFrom + imagesCount <= max;
        }
    }
}