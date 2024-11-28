using Inta.Framework.Ado.Net;
using Inta.Framework.Entity;
using Inta.Framework.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    public class ForgetPasswordController : Controller
    {
        MailManager mailManager = null;
        public ForgetPasswordController()
        {
            mailManager = new MailManager("", "", "", 0, "");
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SendPassword(string email)
        {
            DBLayer dBLayer = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "Email", Value = email });
            var user = dBLayer.Get<SystemUser>("Select * from SystemUser where Email=@Email", System.Data.CommandType.Text, parameters);
            if (user.Data != null)
            {
                List<string> mails = new List<string>();
                mails.Add(user.Data.Email);
                mailManager.Send(mails, "Şifreniz", "Şifreniz : " + user.Data.Password);

                TempData["Error"] = "Şifreniz e-mail adresinize gönderildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Bu E-mail adresi sistemde kayıtlı değil. Lütfen e-mail adresinizi kontrol ediniz.";
                return RedirectToAction("Index");
            }
        }

    }
}