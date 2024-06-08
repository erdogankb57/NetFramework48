using Inta.Framework.Web.Base.Authorization;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Controllers
{
    [AuthorizationCheck]
    public class ImageCropController : Controller
    {
        // GET: ImageCrop
        public ActionResult Index()
        {
            return View();
        }
    }
}