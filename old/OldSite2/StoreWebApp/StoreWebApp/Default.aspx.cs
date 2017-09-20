// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="Default.aspx.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED
{
    using System;
    using System.Collections.Generic;
    using NLog;

    using ShowcaseED;
    using ShowcaseED.Controls;
    using ShowcaseED.Data;
    using ShowcaseED.Interfaces;
    using ShowcaseED.Model;
    using ShowcaseED.Utils;

    /// <summary>
    /// Default page
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// The images count on page
        /// </summary>
        private const int ImagesCountOnPage = 15;

        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Handles the PreRender event of the Default control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void Default_PreRender(object sender, EventArgs e)
        {
            this.DisplayList();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DisplayInfo di = null;
                string path = SiteHelper.TempFilePath(Server);

                this.galleryThumbnails1.Controls.Clear();

                Page.PreRender += this.Default_PreRender;

                this.GetImagesCount();

                if (!Page.IsPostBack)
                {
                    di = this.Session["DisplayInfo"] as DisplayInfo;
                    if (di != null)
                    {
                        di.From = 0;
                        di.Count = ImagesCountOnPage;
                        di.GroupId = this.GetGroupId();

                        var list = new DumpedDataProvider(di.ConnStr, path).LoadMedia(di.GroupId, di.From, di.Count);
                        this.Session["DisplayInfo"] = di;
                        this.Session["DumpedMediaList"] = list;

                        this.GroupsLine1.LinkNext.Visible = di.ImagesCount > di.Count;
                    }
                    else
                    {
                        this.ShowMessage("Unable to find database connection string.");
                    }

                    this.GroupsLine1.LinkPrev.Visible = false;

                    this.LoadCatalog();

                    this.ShowSingleImage();
                }
                else
                {
                    di = this.Session["DisplayInfo"] as DisplayInfo;
                    if (di != null)
                    {
                        di.Count = ImagesCountOnPage;
                        this.Session["DisplayInfo"] = di;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
                this.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Gets the group id.
        /// </summary>
        /// <returns>GroupId value</returns>
        private int GetGroupId()
        {
            int res;
            try
            {
                res = this.Request["catalog"] == null ? 0 : Convert.ToInt32(this.Request["catalog"]);
            }
            catch (Exception ex)
            {
                this.LogError(string.Format("Error while reading 'catalog' from query string. Message: {0}", ex.Message));
                res = 0;
            }

            return res;
        }

        /// <summary>
        /// Shows the single image.
        /// </summary>
        private void ShowSingleImage()
        {
            int itemId;
            if (this.Request["item"] != null)
            {
                itemId = Convert.ToInt32(this.Request["item"]);
                ////string script = @" <script> $(document).ready(function () { $('a.overlayLink').trigger('click'); }) </script>";
                string script = @" <script> $(document).ready(function () { $('.img" + itemId.ToString() + "').trigger('click'); }) </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "AAA", script);
            }
        }

        /// <summary>
        /// Loads the versions.
        /// </summary>
        private void LoadCatalog()
        {
            var di = this.Session["DisplayInfo"] as DisplayInfo;
            if (di != null)
            {
                IStoreDataSource ds = new StoreDataProvider(di.ConnStr);

                this.catalogControl.Controls.Clear();

                var catalog = ds.LoadCatalog();

                foreach (var catalogItem in catalog)
                {
                    var ctrl = LoadControl(@"~\Controls\LinkButtonControl.ascx") as LinkButtonControl;
                    if (ctrl != null)
                    {
                        ctrl.ID = string.Format("CatalogItem_{0}", catalogItem.Id);
                        
                        string pageUrl = Request.Url.AbsoluteUri;
                        
                        if (!string.IsNullOrEmpty(Request.Url.Query))
                        {
                            pageUrl = pageUrl.Replace(Request.Url.Query, string.Empty);
                        }

                        ctrl.Url = string.Format("{0}?catalog={1}", pageUrl, catalogItem.Id);
                        ctrl.Text = catalogItem.Caption;
                        this.catalogControl.Controls.Add(ctrl);
                    }
                }
            }

            Page.DataBind();
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void ShowMessage(string msg)
        {
            ////this.lblMessage.Text = msg;
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void LogMessage(string msg)
        {
            logger.Info(msg);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void LogError(string msg)
        {
            logger.Error(msg);
        }

        /// <summary>
        /// Displays the list.
        /// </summary>
        private void DisplayList()
        {
            try
            {
                var list = Session["DumpedMediaList"] as IEnumerable<DumpedMediaFile>;

                if (list != null)
                {
                    this.LogMessage(string.Format("Found few media entries."));

                    foreach (var dumpedMediaFile in list)
                    {
                        this.LogMessage(string.Format("Displaying object: {0}", ObjectDumper.WriteAsText(dumpedMediaFile)));

                        var img = LoadControl(@"~\Controls\ImageThumbnail.ascx") as ImageThumbnail;
                        if (img != null)
                        {
                            img.Title = string.Format("{0}. [{1}]", dumpedMediaFile.Id, dumpedMediaFile.FileDateTime);
                            img.ID = string.Format("id_{0}", dumpedMediaFile.Id);
                            img.Id = dumpedMediaFile.Id;
                            img.FileUrlFullsize = dumpedMediaFile.Fullsize.GlobalFileUrl;
                            img.FileUrlPreview = dumpedMediaFile.Preview.GlobalFileUrl;
                            img.FileUrlThumbnail = dumpedMediaFile.Thumbnail.GlobalFileUrl;
                            this.galleryThumbnails1.Controls.Add(img);
                        }
                        else
                        {
                            string msg = "Unable to load 'display' control.";
                            this.LogError(msg);
                            this.ShowMessage(msg);
                        }
                    }

                    Page.DataBind();

                    string okayMsg = string.Format("Media content displayed okay at: {0}", DateTime.Now);
                    this.LogMessage(okayMsg);
                    this.ShowMessage(okayMsg);
                }
                else
                {
                    this.GroupsLine1.LinkNext.Visible = false;
                    this.GroupsLine1.LinkNext.Visible = false;
                    string nothingFoundMessage = "DumpedDataProvider did not return list of media files.";
                    this.LogMessage(nothingFoundMessage);
                    this.ShowMessage(nothingFoundMessage);
                }
            }
            catch (Exception ex)
            {
                string nothingFoundMessage = string.Format("No data to display. ", ex.Message);
                this.LogMessage(nothingFoundMessage);
                this.ShowMessage(nothingFoundMessage);
            }
        }

        /// <summary>
        /// Gets the images count.
        /// </summary>
        private void GetImagesCount()
        {
            var di = this.Session["DisplayInfo"] as DisplayInfo;
            if (di != null)
            {
                if (!string.IsNullOrEmpty(di.ConnStr))
                {
                    di.ImagesCount = new DumpedDataProvider(di.ConnStr, SiteHelper.TempFilePath(Server)).GetImagesCount();
                    this.Session["DisplayInfo"] = di;
                }
            }
        }
    }
}