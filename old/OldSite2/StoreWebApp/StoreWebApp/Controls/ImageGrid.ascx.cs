// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="ImageGrid.ascx.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using ShowcaseED.Data;
    using ShowcaseED.Interfaces;
    using ShowcaseED.Model;
    using ShowcaseED.Utils;

    /// <summary>
    /// Image Grid
    /// </summary>
    public partial class ImageGrid : System.Web.UI.UserControl
    {
        /////// <summary>
        /////// The virtual path
        /////// </summary>
        ////private string virtualPath;

        /////// <summary>
        /////// The physical path
        /////// </summary>
        ////private string physicalPath;

        /// <summary>
        /// Gets or sets the image folder path.
        /// </summary>
        /// <value>
        /// The image folder path.
        /// </value>
        public string ImageFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [admin mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [admin mode]; otherwise, <c>false</c>.
        /// </value>
        public bool AdminMode { get; set; }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ////Update the path
         ////   this.UpdatePath();

            ////Show AdminMode specific controls
            if (this.AdminMode)
            {
                this.ImageListView.InsertItemPosition = InsertItemPosition.FirstItem;
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ////Binds the Data Before Rendering
            this.BindData();
            
            var editItem = this.Session["EditingItem"] as MediaDraft;
            if (editItem == null)
            {
                this.pnlEdit.Visible = false;
            }
            else
            {
                this.pnlEdit.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Load event of the titleLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void TitleLabel_Load(object sender, EventArgs e)
        {
            var titleLabel = sender as Label;
            if (titleLabel == null)
            {
                return;
            }

            titleLabel.Text = this.Title;
        }
        
        /// <summary>
        /// Handles the ItemCommand event of the ImageListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListViewCommandEventArgs" /> instance containing the event data.</param>
        protected void ImageListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            /* We have not bound the control to any DataSource derived controls, 
            nor do we use any key to identify the image. Hence, it makes more sense not to 
            use 'delete' but to use a custom command 'Remove' which can be fired as a 
            generic ItemCommand, and the ListViewCommandEventArgs e will have 
            the CommandArgument passed by the 'Remove' button In this case, it is the bound 
            ImageUrl that we are passing, and making use it of to delete the image.*/

            var di = this.Session["DisplayInfo"] as DisplayInfo;
            IStoreDataSource ds = new StoreDataProvider(di.ConnStr);

            switch (e.CommandName)
            {
                case "SelectItem":
                    var itemId = Convert.ToInt32(e.CommandArgument);
                    var mediaObject = ds.LoadMediaFile(itemId);
                    this.Session["EditingItem"] = mediaObject;
                    this.ddlGroup.SelectedValue = mediaObject.GroupId.ToString();
                    break;
                case "Remove":
                    int item = Convert.ToInt32(e.CommandArgument);
                    ds.DeleteImage(item);
                    break;
                case "Update":
                    throw new NotImplementedException();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the Load event of the imageUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ImageUpload_Load(object sender, EventArgs e)
        {
            ////Get the required controls
            var imageUpload = sender as FileUpload;
            if (imageUpload == null)
            {
                return;
            }

            var parent = imageUpload.Parent;
            if (parent == null)
            {
                return;
            }

            var imageUploadStatus = parent.FindControl("imageUploadStatusLabel") as Label;
            if (imageUploadStatus == null)
            {
                return;
            }

            ////If a file is posted, save it
            if (this.IsPostBack)
            {
                if (imageUpload.PostedFile != null && imageUpload.PostedFile.ContentLength > 0)
                {
                    try
                    {
                        var di = this.Session["DisplayInfo"] as DisplayInfo;
                        if (di != null)
                        {
                            IStoreDataSource ds = new StoreDataProvider(di.ConnStr);
                            var image = new byte[imageUpload.PostedFile.ContentLength];
                            var tmpImg = new byte[imageUpload.PostedFile.ContentLength];
                            imageUpload.PostedFile.InputStream.Read(
                                image, 0, Convert.ToInt32(imageUpload.PostedFile.ContentLength));

                            if (image.SequenceEqual(tmpImg))
                            {
                                return;
                            }

                            ds.SaveMedia(new MediaDraft { Media = image, FileSize = imageUpload.PostedFile.ContentLength, ServerFilePath = imageUpload.PostedFile.FileName });
                        }

                        imageUploadStatus.Text = string.Format("Image {0} successfully uploaded!", imageUpload.PostedFile.FileName);
                    }
                    catch (Exception ex)
                    {
                      ////  Logger.Log(ex.Message);
                        imageUploadStatus.Text = string.Format("Error uploading {0}!", imageUpload.PostedFile.FileName);
                    }
                }
                else
                {
                    imageUploadStatus.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var item = this.Session["EditingItem"] as MediaDraft;
            item.GroupId = Convert.ToInt32(this.ddlGroup.SelectedValue);

            var di = this.Session["DisplayInfo"] as DisplayInfo;
            IStoreDataSource ds = new StoreDataProvider(di.ConnStr);
            ds.UpdateMedia(item);

            this.Session["EditingItem"] = null;
        }

        /// <summary>
        /// Gets the list of images2.
        /// </summary>
        /// <returns>List of images</returns>
        private IEnumerable<EditImage> GetListOfImages()
        {
            try
            {
                var tmpList = new List<EditImage>();
                var di = this.Session["DisplayInfo"] as DisplayInfo;
                var list = new DumpedDataProvider(di.ConnStr, SiteHelper.TempFilePath(Server)).LoadMedia(di.GroupId, di.From, di.Count);

                foreach (var item in list)
                {
                    tmpList.Add(new EditImage { Id = item.Id, Url = string.Format("../{0}", item.Thumbnail.GlobalFileUrl), GroupId = item.GroupId });
                }

                return tmpList;
            }
            catch (Exception ex)
            {
                ////log exception
                //// Logger.Log(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets the product groups.
        /// </summary>
        /// <returns>List of product groups</returns>
        private IEnumerable<ProductGroup> GetProductGroups()
        {
            var list = new List<ProductGroup>();
            list.Add(new ProductGroup { Id = 0, Name = "not selected" });

            var di = this.Session["DisplayInfo"] as DisplayInfo;
            if (di != null)
            {
                IStoreDataSource ds = new StoreDataProvider(di.ConnStr);
                list.AddRange(ds.Groups());
                return list;
            }

            return null;
        }

        /////// <summary>
        /////// Updates the path variables
        /////// </summary>
        ////private void UpdatePath()
        ////{
        ////    ////use a default path
        ////    this.virtualPath = SiteHelper.TempFilePath(this.Server);
        ////}

        /// <summary>
        /// Binds the ImageListView to current DataSource
        /// </summary>
        private void BindData()
        {
            this.ImageListView.DataSource = this.GetListOfImages();
            this.ImageListView.DataBind();

            this.ddlGroup.DataSource = this.GetProductGroups();
            this.ddlGroup.DataBind();
        }
    }
}