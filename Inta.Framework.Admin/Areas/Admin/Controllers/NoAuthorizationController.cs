using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    public class NoAuthorizationController : Controller
    {
        // GET: NoAuthorization
        public ActionResult Index()
        {
            return View();
        }
    }
}