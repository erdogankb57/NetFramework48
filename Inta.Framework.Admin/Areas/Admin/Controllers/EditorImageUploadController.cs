using Inta.Framework.Ado.Net;
using Inta.Framework.Entity;
using Inta.Framework.Extension;
using Inta.Framework.Admin.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
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
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string imageUrl = "";
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (generalSettings.Data != null)
                imageUrl = generalSettings.Data.EditorImageUploadPath;

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
                ImageUrl = generalSettings.Data.EditorImageUploadCdn + imageName + "?d=" + guide.ToString()
            });
        }
        public ActionResult GetImageList()
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            string imageFilePath = "";
            var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (generalSettings.Data != null)
                imageFilePath = generalSettings.Data.EditorImageUploadCdn;

            List<FileInfo> imageList = new List<FileInfo>();

            DirectoryInfo d = new DirectoryInfo(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["FileUploadEditor"]));

            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff,*.g01,*.g02,*.g03,*.g04,*.g05,*.g06,*.g07,*.g08";

            var result = d.GetFiles("*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s.FullName).ToLower())).OrderByDescending(f => f.LastWriteTime).Select(s => new { Name = s.Name, FullName = ConfigurationManager.AppSettings["FileUploadEditor"].ToString() + s.Name + "?d=" + Guid.NewGuid().ToString() }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Save(HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                try
                {
                    DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                    string filePath = "";
                    var generalSettings = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
                    if (generalSettings.Data != null)
                        filePath = generalSettings.Data.EditorImageUploadPath;

                    var imageResult = ImageManager.ImageUploadSingleCopy(Image, filePath);

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter { ParameterName = "Name", Value = imageResult });
                    db.ExecuteNoneQuery("insert into EditorImages(Name) values(@Name)", System.Data.CommandType.Text, parameters);
                }
                catch (Exception ex)
                {
                }

            }

            return Json("OK");
        }

    }
}