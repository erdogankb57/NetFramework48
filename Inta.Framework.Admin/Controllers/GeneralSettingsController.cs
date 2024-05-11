using Inta.Framework.Admin.Models;
using Inta.Framework.Ado.Net;
using Inta.Framework.Contract;
using Inta.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
            return View(model);
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

            if (string.IsNullOrEmpty(request.CdnUrl))
                parameters.Add(new SqlParameter { ParameterName = "CdnUrl", Value = DBNull.Value });
            else
                parameters.Add(new SqlParameter { ParameterName = "CdnUrl", Value = request.CdnUrl });

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

            parameters.Add(new SqlParameter { ParameterName = "CategoryImageSmallWidth", Value = request.CategoryImageSmallWidth });
            parameters.Add(new SqlParameter { ParameterName = "CategoryImageSmallHeight", Value = request.CategoryImageSmallHeight });
            parameters.Add(new SqlParameter { ParameterName = "CategoryImageBigWidth", Value = request.CategoryImageBigWidth });
            parameters.Add(new SqlParameter { ParameterName = "CategoryImageBigHeight", Value = request.CategoryImageBigHeight });
            parameters.Add(new SqlParameter { ParameterName = "ContentImageSmallWidth", Value = request.ContentImageSmallWidth });
            parameters.Add(new SqlParameter { ParameterName = "ContentImageSmallHeight", Value = request.ContentImageSmallHeight });
            parameters.Add(new SqlParameter { ParameterName = "ContentImageBigWidth", Value = request.ContentImageBigWidth });
            parameters.Add(new SqlParameter { ParameterName = "ContentImageBigHeight", Value = request.ContentImageBigHeight });

            if (request.Id == 0)
            {

            }
            else
            {

            }

            return Json(data);
        }
    }
}