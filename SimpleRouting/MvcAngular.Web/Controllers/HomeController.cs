using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAngular.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            return PartialView();
        }
    }
}
