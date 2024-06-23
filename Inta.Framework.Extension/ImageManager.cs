using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Inta.Framework.Extension
{
    public class ImageManager
    {
        public static string ImageUploadDoubleCopy(HttpPostedFileBase ImageFile, string filePath, int SmallImageWidth, int BigImageWidth)
        {

            string ImageName = "";
            string extension = System.IO.Path.GetExtension(ImageFile.FileName.ToLower());
            string imageFilePath = filePath;

            string random = ImageFile.FileName.Replace(extension, "") + "_" + Guid.NewGuid().ToString();
            random = StringManager.TextUrlCharReplace(random);

            if (ImageFile.ContentLength > 0 && (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png") && (ImageFile.FileName.ToLower().IndexOf(";") == -1))
            {
                if (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png")
                {
                    ImageFile.SaveAs(imageFilePath + random + extension);

                    System.Drawing.Image imgPhotoVert = System.Drawing.Image.FromFile(imageFilePath + random + extension);
                    System.Drawing.Image imgPhoto = null;

                    imgPhoto = ImageResize(imgPhotoVert, SmallImageWidth);

                    if (extension == ".jpg" | extension == ".jpeg")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else if (extension == ".gif")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Gif);
                    else if (extension == ".png")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Png);
                    else
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);

                    System.Drawing.Image imgPhotoBig = null;

                    imgPhotoBig = ImageResize(imgPhotoVert, BigImageWidth);

                    if (extension == ".jpg" | extension == ".jpeg")
                        imgPhotoBig.Save(imageFilePath + "b_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else if (extension == ".gif")
                        imgPhotoBig.Save(imageFilePath + "b_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Gif);
                    else if (extension == ".png")
                        imgPhotoBig.Save(imageFilePath + "b_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Png);
                    else
                        imgPhotoBig.Save(imageFilePath + "b_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);

                    imgPhotoVert.Dispose();

                    ImageName = random.ToString() + extension;
                }
            }

            return ImageName;
        }
        public static string ImageUploadSingleCopy(HttpPostedFileBase ImageFile, string filePath, int ImageWidth)
        {
            string ImageName = "";
            string extension = System.IO.Path.GetExtension(ImageFile.FileName.ToLower());
            string imageFilePath = filePath;

            string random = ImageFile.FileName.Replace(extension, "") + "_" + Guid.NewGuid().ToString();
            random = StringManager.TextUrlCharReplace(random);

            if (ImageFile.ContentLength > 0 && (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png") && (ImageFile.FileName.ToLower().IndexOf(";") == -1))
            {
                if (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png")
                {
                    ImageFile.SaveAs(imageFilePath + random + extension);

                    System.Drawing.Image imgPhotoVert = System.Drawing.Image.FromFile(imageFilePath + random + extension);
                    System.Drawing.Image imgPhoto = null;

                    imgPhoto = ImageResize(imgPhotoVert, ImageWidth);

                    if (extension == ".jpg" | extension == ".jpeg")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else if (extension == ".gif")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Gif);
                    else if (extension == ".png")
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Png);
                    else
                        imgPhoto.Save(imageFilePath + "k_" + random.ToString() + extension, System.Drawing.Imaging.ImageFormat.Jpeg);

                    imgPhotoVert.Dispose();

                    ImageName = random.ToString() + extension;
                }
            }

            return ImageName;
        }

        public static string ImageUploadSingleCopy(HttpPostedFileBase ImageFile, string filePath)
        {
            string ImageName = "";
            string extension = System.IO.Path.GetExtension(ImageFile.FileName.ToLower());
            string imageFilePath = filePath;

            string random = ImageFile.FileName.Replace(extension, "") + "_" + Guid.NewGuid().ToString();
            random = StringManager.TextUrlCharReplace(random);

            if (ImageFile.ContentLength > 0 && (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png") && (ImageFile.FileName.ToLower().IndexOf(";") == -1))
            {
                if (extension == ".jpg" | extension == ".jpeg" | extension == ".gif" | extension == ".png")
                {
                    ImageFile.SaveAs(imageFilePath + random + extension);

                    ImageName = random.ToString() + extension;
                }
            }

            return ImageName;
        }

        private static System.Drawing.Image ImageResize(System.Drawing.Image imgPhoto, int yukseklik)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            int destWidth = yukseklik;
            int destHeight = sourceHeight * yukseklik / imgPhoto.Width;

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;

            grPhoto.FillRectangle(Brushes.Transparent, 0, 0, destWidth, destHeight);

            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(0, 0, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        
    }
}
