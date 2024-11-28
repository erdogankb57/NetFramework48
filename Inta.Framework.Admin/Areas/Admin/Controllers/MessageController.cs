using Inta.Framework.Admin.Base.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Admin.Areas.Admin.Controllers
{
    public class MessageModel
    {
        public string RedirectUrl { get; set; }
        public string Message { get; set; }
    }
    [AuthorizationCheck]
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Success(MessageModel request)
        {
            ViewBag.RedirectUrl = request.RedirectUrl;
            ViewBag.Message = request.Message;

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}