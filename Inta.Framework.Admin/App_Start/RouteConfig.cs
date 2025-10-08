using Inta.Framework.Business.Manager;
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


            //Detay sayfası url yapısı hakkimizda/1.html şeklinde olacak
            routes.MapRoute(
                 name: "Detail",
                 url: "{url}/{id}.html",
                 defaults: new { url = UrlParameter.Optional, controller = "Detail", action = "Index", id = UrlParameter.Optional },
                 new string[] { "Inta.Framework.Web.Controllers" }
             );

            //Url çağırılırken category/url/id şeklinde çağırılmalıdır.

            var result = categoryManager.Find();
            foreach (var item in result)
            {
                if (!string.IsNullOrEmpty(item.CategoryFullUrl))
                {
                    routes.MapRoute(
                    name: "CategoryDefault" + item.Id.ToString(),
                    url: item.CategoryFullRouting,
                    defaults: new { controller = item.ControllerName, action = item.ActionName, id = UrlParameter.Optional },
                    new string[] { "Inta.Framework.Web.Controllers" }
    );
                }
            }



            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new string[] { "Inta.Framework.Web.Controllers" }
             );
        }


    }
}
