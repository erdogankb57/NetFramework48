using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Inta.Framework.Web.Base.Authorization
{
    public class AuthenticationData
    {
        public AuthenticationData()
        {
        }

        public int LanguageId
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("languageId"))
                    return Convert.ToInt32(GetAuthenticationData["languageId"]);
                else
                    return 0;
            }
        }

        public string UserName
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("userName"))
                    return GetAuthenticationData["userName"].ToString();
                else
                    return "";
            }
        }

        public string UserId
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("userId"))
                    return GetAuthenticationData["userId"].ToString();
                else
                    return "";
            }
        }

        public string Password
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("password"))
                    return GetAuthenticationData["password"].ToString();
                else
                    return "";
            }
        }
        public string LoginDate
        {
            get
            {
                if (GetAuthenticationData.ContainsKey("loginDate"))
                    return GetAuthenticationData["loginDate"].ToString();
                else
                    return "";
            }
        }
        public bool HasSession
        {
            get
            {
                if (GetAuthenticationData.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public void SetLangugageId(string id)
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

        private Dictionary<string, string> GetAuthenticationData
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
