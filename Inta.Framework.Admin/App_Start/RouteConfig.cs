using Inta.Framework.Ado.Net;
using Inta.Framework.Web.Manager;
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

            CategoryManager categoryManager = new CategoryManager();
            //Url çağırılırken category/url/id şeklinde çağırılmalıdır.

            var result = categoryManager.FindCategoryPageType();
            foreach (var item in result)
            {
                if (!string.IsNullOrEmpty(item.CategoryFullUrl))
                {
                    routes.MapRoute(
                    name: "CategoryDefault" + item.Id.ToString(),
                    url: item.CategoryFullUrl,
                    defaults: new { controller = item.ControllerName, action = item.ActionName, id = UrlParameter.Optional },
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


    }
}
