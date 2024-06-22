using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using Inta.Framework.Admin.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
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
            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Save(GeneralSettings request)
        {
            ReturnObject<GeneralSettings> data = new ReturnObject<GeneralSettings>();

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();

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

            if (string.IsNullOrEmpty(request.ImageCdnUrl))
                parameters.Add(new SqlParameter { ParameterName = "ImageCdnUrl", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "ImageCdnUrl", Value = request.ImageCdnUrl });

            if (string.IsNullOrEmpty(request.FileCdnUrl))
                parameters.Add(new SqlParameter { ParameterName = "FileCdnUrl", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "FileCdnUrl", Value = request.FileCdnUrl });

            if (string.IsNullOrEmpty(request.ImageUploadPath))
                parameters.Add(new SqlParameter { ParameterName = "ImageUploadPath", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "ImageUploadPath", Value = request.ImageUploadPath });

            if (string.IsNullOrEmpty(request.FileUploadPath))
                parameters.Add(new SqlParameter { ParameterName = "FileUploadPath", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "FileUploadPath", Value = request.FileUploadPath });

            if (string.IsNullOrEmpty(request.DeveloperName))
                parameters.Add(new SqlParameter { ParameterName = "DeveloperName", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "DeveloperName", Value = request.DeveloperName });

            if (string.IsNullOrEmpty(request.DeveloperEmail))
                parameters.Add(new SqlParameter { ParameterName = "DeveloperEmail", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "DeveloperEmail", Value = request.DeveloperEmail });

            if (string.IsNullOrEmpty(request.EditorImageUploadCdn))
                parameters.Add(new SqlParameter { ParameterName = "EditorImageUploadCdn", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "EditorImageUploadCdn", Value = request.EditorImageUploadCdn });

            if (string.IsNullOrEmpty(request.EditorImageUploadPath))
                parameters.Add(new SqlParameter { ParameterName = "EditorImageUploadPath", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "EditorImageUploadPath", Value = request.EditorImageUploadPath });


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


            db.ExecuteNoneQuery(@"
            Update GeneralSettings
            set
            EmailIpAdress=@EmailIpAdress,
            EmailAdress=@EmailAdress,
            EmailPort=@EmailPort,
            EmailPassword=@EmailPassword,
            DomainName=@DomainName,
            ImageCdnUrl=@ImageCdnUrl,
            FileCdnUrl=@FileCdnUrl,
            ImageUploadPath=@ImageUploadPath,
            FileUploadPath=@FileUploadPath,
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
            GalleryImageBigHeight=@GalleryImageBigHeight,
            EditorImageUploadCdn=@EditorImageUploadCdn,
            EditorImageUploadPath=@EditorImageUploadPath
            where Id=@Id    
            ", System.Data.CommandType.Text, parameters);

            //return Json(new ReturnObject<GeneralSettings>
            //{
            //    Data = request,
            //    ResultType = MessageType.Success
            //});


            return Json(data);
        }
    }
}