﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamRunaway.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			BL.Class1.ExtractWayPoints();
            return View("Index");
        }

    }
}
