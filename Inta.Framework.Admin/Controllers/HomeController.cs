﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inta.Framework.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? id)
        {
            return View();
        }
    }
}