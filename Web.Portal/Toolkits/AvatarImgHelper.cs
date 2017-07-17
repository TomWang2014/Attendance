//------------------------------------------------------------
// <copyright file="AvatarImgHelper.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/25 11:26:49</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// 上传头像帮助
    /// </summary>
    public class AvatarImgHelper
    {
        /// <summary>
        /// The crop image.
        /// </summary>
        /// <param name="image"> The image.  </param>
        /// <param name="height"> The height  </param>
        /// <param name="width"> The width.  </param>
        /// <returns> 返Image </returns>
        public static Image CropImage(Image image, int height, int width)
        {
            return CropImage(image, height, width, 0, 0);
        }

        /// <summary>
        /// 需要裁剪的图片流文件
        /// </summary>
        /// <param name="image">图片流文件</param>
        /// <param name="height">高</param>
        /// <param name="width">宽</param>
        /// <param name="startAtX">x</param>
        /// <param name="startAtY">y</param>
        /// <returns>返回裁剪后的图片</returns>
        public static Image CropImage(Image image, int height, int width, int startAtX, int startAtY)
        {
            try
            {
                // check the image height against our desired image height
                if (image.Height < height)
                {
                    height = image.Height;
                }

                if (image.Width < width)
                {
                    width = image.Width;
                }

                // create a bitmap window for cropping
                var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);

                // create a new graphics object from our image and set properties
                var grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // now do the crop
                grPhoto.DrawImage(image, new Rectangle(0, 0, width, height), startAtX, startAtY, width, height, GraphicsUnit.Pixel);

                // Save out to memory and get an image from it to send back out the method.
                var mm = new MemoryStream();
                bmPhoto.Save(mm, ImageFormat.Jpeg);
                image.Dispose();
                bmPhoto.Dispose();
                grPhoto.Dispose();
                var outimage = Image.FromStream(mm);

                return outimage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }


        /// <summary>
        /// 裁减图标
        /// </summary>
        /// <param name="Width">Width</param>
        /// <param name="Height">Height</param>
        /// <param name="image">Image</param>
        /// <returns>返回图片留</returns>
        public static Image HardResizeImage(int Width, int Height, Image image)
        {
            var width = image.Width;
            var height = image.Height;
            Image resized = null;
            if (Width > Height)
            {
                resized = ResizeImage(Width, Width, image);
            }
            else
            {
                resized = ResizeImage(Height, Height, image);
            }

            var output = CropImage(resized, Height, Width);

            // return the original resized image
            return output;
        }

        /// <summary>
        /// The resize image.
        /// </summary>
        /// <param name="maxWidth"> The max width. </param>
        /// <param name="maxHeight"> The max height. </param>
        /// <param name="Image"> The image. </param>
        /// <returns> The <see cref="Image"/>.        /// </returns>
        public static Image ResizeImage(int maxWidth, int maxHeight, Image Image)
        {
            var width = Image.Width;
            var height = Image.Height;
            if (width <= maxWidth && height <= maxHeight)
            {
                return Image;
            }

            // The flips are in here to prevent any embedded image thumbnails -- usually from cameras
            // from displaying as the thumbnail image later, in other words, we want a clean
            // resize, not a grainy one.
            Image.RotateFlip(RotateFlipType.Rotate180FlipX);
            Image.RotateFlip(RotateFlipType.Rotate180FlipX);

            float ratio;
            if (width > height)
            {
                ratio = width / (float)height;
                width = maxWidth;
                height = Convert.ToInt32(Math.Round(width / ratio));
            }
            else
            {
                ratio = height / (float)width;
                height = maxHeight;
                width = Convert.ToInt32(Math.Round(height / ratio));
            }

            // return the resized image
            return Image.GetThumbnailImage(width, height, null, IntPtr.Zero);

            // return the original resized image
        }
    }
}