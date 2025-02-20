using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
using Inta.Framework.Extension;

namespace Inta.Framework.Web.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class GeneralSettingsController : Controller
    {
        // GET: GeneralSettings
        public ActionResult Index()
        {
            GeneralSettings model = new GeneralSettings();
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            var result = db.Get<GeneralSettings>("Select top 1 * from GeneralSettings", System.Data.CommandType.Text);
            if (result.Data != null)
                ViewBag.ImageFolder = ConfigurationManager.AppSettings["ImageUpload"];
            
            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Save(GeneralSettings request, HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                ReturnObject<GeneralSettings> data = new ReturnObject<GeneralSettings>();

                DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();



                if (Logo != null)
                {
                    request.Logo = ImageManager.ImageUploadSingleCopy(Logo, Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]));
                    parameters.Add(new SqlParameter { ParameterName = "Logo", Value = request.Logo });
                }


                if (string.IsNullOrEmpty(request.EmailIpAdress))
                    parameters.Add(new SqlParameter { ParameterName = "EmailIpAdress", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "EmailIpAdress", Value = request.EmailIpAdress });

                if (string.IsNullOrEmpty(request.EmailAdress))
                    parameters.Add(new SqlParameter { ParameterName = "EmailAdress", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "EmailAdress", Value = request.EmailAdress });

                parameters.Add(new SqlParameter { ParameterName = "EmailPort", Value = request.EmailPort });

                if (string.IsNullOrEmpty(request.EmailPassword))
                    parameters.Add(new SqlParameter { ParameterName = "EmailPassword", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "EmailPassword", Value = request.EmailPassword });

                if (string.IsNullOrEmpty(request.DomainName))
                    parameters.Add(new SqlParameter { ParameterName = "DomainName", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "DomainName", Value = request.DomainName });

                if (string.IsNullOrEmpty(request.DeveloperName))
                    parameters.Add(new SqlParameter { ParameterName = "DeveloperName", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "DeveloperName", Value = request.DeveloperName });

                if (string.IsNullOrEmpty(request.DeveloperEmail))
                    parameters.Add(new SqlParameter { ParameterName = "DeveloperEmail", Value = DBNull.Value });
                else
                    parameters.Add(new SqlParameter { ParameterName = "DeveloperEmail", Value = request.DeveloperEmail });

                parameters.Add(new SqlParameter { ParameterName = "CategoryImageSmallWidth", Value = request.CategoryImageSmallWidth });
                parameters.Add(new SqlParameter { ParameterName = "CategoryImageSmallHeight", Value = request.CategoryImageSmallHeight });
                parameters.Add(new SqlParameter { ParameterName = "CategoryImageBigWidth", Value = request.CategoryImageBigWidth });
                parameters.Add(new SqlParameter { ParameterName = "CategoryImageBigHeight", Value = request.CategoryImageBigHeight });
                parameters.Add(new SqlParameter { ParameterName = "ContentImageSmallWidth", Value = request.ContentImageSmallWidth });
                parameters.Add(new SqlParameter { ParameterName = "ContentImageSmallHeight", Value = request.ContentImageSmallHeight });
                parameters.Add(new SqlParameter { ParameterName = "ContentImageBigWidth", Value = request.ContentImageBigWidth });
                parameters.Add(new SqlParameter { ParameterName = "ContentImageBigHeight", Value = request.ContentImageBigHeight });

                parameters.Add(new SqlParameter { ParameterName = "GalleryImageSmallWidth", Value = request.GalleryImageSmallWidth });
                parameters.Add(new SqlParameter { ParameterName = "GalleryImageSmallHeight", Value = request.GalleryImageSmallHeight });
                parameters.Add(new SqlParameter { ParameterName = "GalleryImageBigWidth", Value = request.GalleryImageBigWidth });
                parameters.Add(new SqlParameter { ParameterName = "GalleryImageBigHeight", Value = request.GalleryImageBigHeight });


                parameters.Add(new SqlParameter { ParameterName = "Id", Value = request.Id });

                string query = @"
            Update GeneralSettings
            set
            EmailIpAdress=@EmailIpAdress,
            EmailAdress=@EmailAdress,
            EmailPort=@EmailPort,
            EmailPassword=@EmailPassword,
            DomainName=@DomainName,
            DeveloperName=@DeveloperName,
            DeveloperEmail=@DeveloperEmail,
            CategoryImageSmallWidth=@CategoryImageSmallWidth,
            CategoryImageSmallHeight=@CategoryImageSmallHeight,
            CategoryImageBigWidth=@CategoryImageBigWidth,
            CategoryImageBigHeight=@CategoryImageBigHeight,
            ContentImageSmallWidth=@ContentImageSmallWidth,
            ContentImageSmallHeight=@ContentImageSmallHeight,
            ContentImageBigWidth=@ContentImageBigWidth,
            ContentImageBigHeight=@ContentImageBigHeight,
            GalleryImageSmallWidth=@GalleryImageSmallWidth,
            GalleryImageSmallHeight=@GalleryImageSmallHeight,
            GalleryImageBigWidth=@GalleryImageBigWidth,
            GalleryImageBigHeight=@GalleryImageBigHeight,";
                if (Logo != null)
                {
                    query += " Logo=@Logo";

                }
                query += " where Id=@Id";
                ;
                db.ExecuteNoneQuery(query, System.Data.CommandType.Text, parameters);

                string RedirectUrl = "/Admin/GeneralSettings/Index";
                return RedirectToAction("Success", "Message", new { area = "Admin", RedirectUrl = RedirectUrl, Message = "Kayıt güncelleme işlemi başarıyla tamamlandı" });
            }
            else
            {
                return View("Index", request);
            }
        }

        [HttpPost]
        public ActionResult DeleteImage(string id)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Id", Value = id });
            var banner = db.Get<GeneralSettings>("select * from GeneralSettings where Id=" + Convert.ToInt32(id), System.Data.CommandType.Text).Data;
            if (banner != null && !string.IsNullOrEmpty(banner.Logo))
            {
                DeleteImageFile(banner.Logo);
                var result = db.ExecuteNoneQuery("Update GeneralSettings set Logo='' where Id=@Id", System.Data.CommandType.Text, parameters);
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        private void DeleteImageFile(string Image)
        {
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());

            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "k_" + Image);

            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + "b_" + Image);

            if (System.IO.File.Exists(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + Image))
                System.IO.File.Delete(Server.MapPath(ConfigurationManager.AppSettings["ImageUpload"]) + "\\" + Image);

        }
    }
}