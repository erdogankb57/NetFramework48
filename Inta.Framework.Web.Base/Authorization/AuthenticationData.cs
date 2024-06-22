using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Inta.Framework.Admin.Base.Authorization
{
    public static class AuthenticationData
    {
        public static int LanguageId
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("languageId"))
                    return Convert.ToInt32(GetAuthenticationData["languageId"]);
                else
                    return 0;
            }
        }

        public static string UserName
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("userName"))
                    return GetAuthenticationData["userName"].ToString();
                else
                    return "";
            }
        }

        public static string UserId
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("userId"))
                    return GetAuthenticationData["userId"].ToString();
                else
                    return "";
            }
        }

        public static string Password
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("password"))
                    return GetAuthenticationData["password"].ToString();
                else
                    return "";
            }
        }
        public static string LoginDate
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("loginDate"))
                    return GetAuthenticationData["loginDate"].ToString();
                else
                    return "";
            }
        }

        public static bool IsAdmin
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("IsAdmin"))
                    return Convert.ToBoolean(GetAuthenticationData["IsAdmin"].ToString());
                else
                    return false;
            }
        }
        public static bool HasSession
        {
            get
            {
                if (GetAuthenticationData.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public static void SetLangugageId(string id)
        {
            if (HttpContext.Current.Session["AuthData"] != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                var authKey = serializer.Deserialize<Dictionary<string, string>>(HttpContext.Current.Session["AuthData"].ToString());
                if (authKey.ContainsKey("languageId"))
                {
                    authKey["languageId"] = id.ToString();

                    var authData = serializer.Serialize(authKey);

                    HttpContext.Current.Session["AuthData"] = authData;
                }

            }
        }

        private static Dictionary<string, string> GetAuthenticationData
        {
            get
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (HttpContext.Current.Session["AuthData"] != null)
                    return serializer.Deserialize<Dictionary<string, string>>(HttpContext.Current.Session["AuthData"].ToString());
                //else if (HttpContext.Current.Session["AuthData"] != null)
                //    serializer.Deserialize<Dictionary<string, string>>(HttpCookie["AuthData"].ToString());
                else
                    return new Dictionary<string, string>();
            }
        }


    }
}
