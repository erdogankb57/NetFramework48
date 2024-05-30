﻿using Inta.Framework.Extension;
using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    [AuthorizationCheck]
    public class EditorImageUploadController : Controller
    {
        public EditorImageUploadController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CropImage(string imageName, int width, int height, int x, int y)
        {
            string imageUrl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]);

            // Create a new image at the cropped size
            Bitmap cropped = new Bitmap(width, height);

            //Load image from file
            using (Image image = Image.FromFile(imageUrl + imageName))
            {
                // Create a Graphics object to do the drawing, *with the new bitmap as the target*
                using (Graphics g = Graphics.FromImage(cropped))
                {
                    // Draw the desired area of the original into the graphics object
                    g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                    image.Dispose();
                    g.Dispose();
                    // Save the result
                    cropped.Save(imageUrl + imageName);
                    cropped.Dispose();
                }
            }
            Guid guide = Guid.NewGuid();

            return Json(new
            {
                ResultMessage = "OK",
                ImageUrl = ConfigurationManager.AppSettings["FileUploadEditor"].ToString() + imageName + "?d=" + guide.ToString()
            });
        }
        public ActionResult GetImageList()
        {

            string imageFilePath = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]);

            List<FileInfo> imageList = new List<FileInfo>();

            DirectoryInfo d = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]));

            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff,*.g01,*.g02,*.g03,*.g04,*.g05,*.g06,*.g07,*.g08";

            var result = d.GetFiles("*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s.FullName).ToLower())).OrderByDescending(f => f.LastWriteTime).Select(s => new { Name = s.Name, FullName = ConfigurationManager.AppSettings["FileUploadEditor"].ToString() + s.Name + "?d=" + Guid.NewGuid().ToString() }).ToList();

            return Json(result,JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                string filePath = ConfigurationManager.AppSettings["FileUploadEditor"];
                var imageResult = ImageManager.ImageUploadSingleCopy(Image, filePath);
            }

            return Json("OK");
        }

    }
}