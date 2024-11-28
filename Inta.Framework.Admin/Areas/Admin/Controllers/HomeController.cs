using Inta.Framework.Admin.Base.Authorization;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    [AuthorizationCheck]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}