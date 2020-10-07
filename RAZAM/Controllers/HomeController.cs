using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAZAM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Notes()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }
        public ActionResult Files()
        {
            return View();
        }
    }
}