using Inta.Framework.Web.Models;
using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Inta.Framework.Web.Areas.Admin.Models;

namespace Inta.Framework.Web.Areas.Admin.Controllers
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
        public ActionResult SignIn(LoginModel request)
        {
            if (ModelState.IsValid)
            {
                DBLayer dbLayer = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter { ParameterName = "UserName", Value = request.UserName });
                parameters.Add(new SqlParameter { ParameterName = "Password", Value = request.Password });

                var user = dbLayer.Get("Select * from SystemUser where (UserName=@UserName or Email=@UserName) and Password=@Password and IsActive=1", System.Data.CommandType.Text, parameters);
                if (user.Data != null)
                {
                    if (String.IsNullOrEmpty(request.LanguageId))
                    {
                        var lang = dbLayer.Get("Select * from Language where IsActive=1 Order by Id desc", System.Data.CommandType.Text, parameters);
                        request.LanguageId = lang.Data != null ? lang.Data["Id"].ToString() : "0";
                    }

                    Dictionary<string, string> authKey = new Dictionary<string, string>();
                    authKey.Add("userName", user.Data["UserName"].ToString());
                    authKey.Add("password", user.Data["Password"].ToString());
                    authKey.Add("loginDate", DateTime.Now.ToString());
                    authKey.Add("languageId", request.LanguageId);
                    authKey.Add("userId", user.Data["Id"].ToString());
                    authKey.Add("IsAdmin", user.Data["IsAdmin"].ToString());



                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var authData = serializer.Serialize(authKey);

                    HttpContext.Session["AuthData"] = authData;

                    //if (request.CreatePersistentCookie == true)
                    //{
                    //    HttpCookie cookie = new HttpCookie("AuthData", authData);
                    //    cookie.Expires = DateTime.Now.AddDays(1);
                    //    HttpContext.Response.Cookies.Add(cookie);
                    //}

                    if (!string.IsNullOrEmpty(request.ReturnUrl))
                    {
                        return Json(new { Status = "OK", ReturnUrl = request.ReturnUrl, Message = "" });
                    }
                    else
                        return Json(new { Status = "OK", ReturnUrl = "/Admin/Home", Message = "" });

                }
                else
                {
                    return Json(new { Status = "Error", ReturnUrl = request.ReturnUrl, Message = "Kullanıcı adı veya şifre hatalı." });
                }
            }
            else
            {
                return Json(new
                {
                    Data = request,
                    Status = "Error",
                    Validation = ModelState.ToList().Where(v => v.Value.Errors.Any()).Select(s => new { Key = s.Key, Error = s.Value.Errors })
                });
            }
        }
        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Remove("AuthData");
            return Redirect("/Admin/Login");
        }

    }
}