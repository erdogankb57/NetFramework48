using Inta.Framework.Ado.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inta.Framework.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            var urlList = db.Find("select distinct p.ControllerName,p.ActionName,c.Id,c.CategoryUrl from Category c inner join PageType p on c.PageTypeId=p.Id", System.Data.CommandType.Text);

            //Url çağırılırken category/url/id şeklinde çağırılmalıdır.
            for (int i = 0; i < urlList.Data.Rows.Count; i++)
            {
                string url = GetCategoryUrl(Convert.ToInt32(urlList.Data.Rows[i]["Id"]));
                if (!string.IsNullOrEmpty(url))
                {
                    routes.MapRoute(
                    name: "CategoryDefault" + urlList.Data.Rows[i]["Id"].ToString(),
                    url: "category/" + url + "/{id}",
                    defaults: new { controller = urlList.Data.Rows[i]["ControllerName"], action = urlList.Data.Rows[i]["ActionName"], id = UrlParameter.Optional },
                    new string[] { "Inta.Framework.Admin.Controllers" }
    );
                }
            }

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new string[] { "Inta.Framework.Admin.Controllers" }
             );
        }

        public static string GetCategoryUrl(int Id)
        {
            string url = "";
            DBLayer db = new DBLayer(ConfigurationManager.ConnectionStrings["DefaultDataContext"].ToString());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter { ParameterName = "CategoryId", Value = Id });
            var category = db.Get("Select * from Category where Id=@CategoryId", System.Data.CommandType.Text, parameters);

            if (category.Data != null)
            {
                if (category.Data["CategoryId"] != null && Convert.ToInt32(category.Data["CategoryId"]) != 0)
                {
                    url = "" + category.Data["CategoryUrl"].ToString();
                    url = GetCategoryUrl(Convert.ToInt32(category.Data["CategoryId"])) + url;
                }
                else
                {
                    url = "" + category.Data["CategoryUrl"].ToString();
                }
            }

            return url;
        }
    }
}
