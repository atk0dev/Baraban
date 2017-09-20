// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="FileDumper.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Data
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using NLog;
    using ShowcaseED.Model;

    /// <summary>
    /// File Dumper
    /// </summary>
    public class FileDumper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public DumpedMediaFile File { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        /// Dumps this instance.
        /// </summary>
        public void Dump()
        {
            try
            {
                ////save to disk
                using (var fs = new FileStream(this.File.Fullsize.LocalFilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(this.Data, 0, this.Size);
                    fs.Close();
                }

                Image original = Image.FromFile(this.File.Fullsize.LocalFilePath);

                ////save thumbnail image
                Image thumbnail = this.ResizeImage(original, new Size(75, 75));
                thumbnail.Save(this.File.Thumbnail.LocalFilePath);

                ////save preview image
                Image preview = this.ResizeImage(original, new Size(375, 375));
                preview.Save(this.File.Preview.LocalFilePath);
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("Error while saving file for [{0}] Media ID. Message: {1}", this.File.Id, ex.Message));
            }
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="size">The size.</param>
        /// <param name="preserveAspectRatio">if set to <c>true</c> [preserve aspect ratio].</param>
        /// <returns>Resized image</returns>
        private Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
    }
}