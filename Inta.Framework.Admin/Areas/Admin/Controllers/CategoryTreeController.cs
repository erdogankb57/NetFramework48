using Inta.Framework.Web.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Web.Areas.Admin.Controllers
{

    [AuthorizationCheck]
    public class CategoryTreeController : Controller
    {
        // GET: CategoryTree
        public ActionResult Index()
        {
            return View();
        }
    }
}