using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Inta.Framework.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.ReturnUrl = Request["ReturnUrl"] != null ? Request["ReturnUrl"].ToString() : "";

            return View();
        }

        [AllowAnonymous]
        public ActionResult SignIn(string userName, string password, string LanguageId, bool? createPersistentCookie, string ReturnUrl)
        {
            DBLayer dbLayer = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "UserName", Value = userName });
            parameters.Add(new SqlParameter { ParameterName = "Password", Value = password });

            var user = dbLayer.Get("Select * from SystemUser where (UserName=@UserName or Email=@UserName) and Password=@Password and IsActive=1", System.Data.CommandType.Text, parameters);
            if (user.Data != null)
            {
                if (String.IsNullOrEmpty(LanguageId))
                {
                    var lang = dbLayer.Get("Select * from Language where IsActive=1 Order by Id desc", System.Data.CommandType.Text, parameters);
                    LanguageId = lang.Data != null ? lang.Data["Id"].ToString() : "0";
                }

                Dictionary<string, string> authKey = new Dictionary<string, string>();
                authKey.Add("userName", user.Data["UserName"].ToString());
                authKey.Add("password", user.Data["Password"].ToString());
                authKey.Add("loginDate", DateTime.Now.ToString());
                authKey.Add("languageId", LanguageId);
                authKey.Add("userId", user.Data["Id"].ToString());


                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var authData = serializer.Serialize(authKey);

                HttpContext.Session["AuthData"] = authData;

                if (createPersistentCookie == true)
                {
                    HttpCookie cookie = new HttpCookie("AuthData", authData);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                }

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Json(new { Status = "OK", ReturnUrl = ReturnUrl, Message = "" });
                }
                else
                    return Json(new { Status = "OK", ReturnUrl = "/Home", Message = "" });

            }
            else
            {
                return Json(new { Status = "Error", ReturnUrl = ReturnUrl, Message = "Kullanıcı adı veya şifre hatalı." });
            }
        }
        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Remove("AuthData");
            return RedirectToAction("Index", "Login");
        }

    }
}