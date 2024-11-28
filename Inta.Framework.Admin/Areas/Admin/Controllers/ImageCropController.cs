using Inta.Framework.Ado.Net;
using Inta.Framework.Entity;
using Inta.Framework.Admin.Base.Authorization;
using System;
using System.Configuration;
using System.Drawing;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class ImageCropController : Controller
    {
        // GET: ImageCrop
        public ActionResult Index()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);


            ViewBag.Image = generalSettings.Data.ImageCdnUrl + Request["Dimension"] + Request["ImageName"];
            ViewBag.Width = Request["width"];
            ViewBag.Height = Request["height"];
            return View();
        }

        public ActionResult CropImage(string imageName, int width, int height, int x, int y)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string imageUrl = "";
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (generalSettings.Data != null)
                imageUrl = generalSettings.Data.ImageUploadPath;

            //string imageUrl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]);

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
                ImageUrl = generalSettings.Data.ImageCdnUrl + imageName + "?d=" + guide.ToString()
            });
        }

        public ActionResult ReturnImage(string imageName)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string imageUrl = "";
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (generalSettings.Data != null)
                imageUrl = generalSettings.Data.ImageUploadPath;

            //string imageUrl = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]);

            // Create a new image at the cropped size

            //Load image from file
            using (Image image = Image.FromFile(imageUrl + imageName.Substring(2,imageName.Length-2)))
            {
                image.Save(imageUrl + imageName);
            }
            Guid guide = Guid.NewGuid();

            return Json(new
            {
                ResultMessage = "OK",
                ImageUrl = generalSettings.Data.ImageCdnUrl + imageName + "?d=" + guide.ToString()
            });
        }

    }
}