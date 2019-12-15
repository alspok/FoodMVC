using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Apie mus";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Adresas, telefonas, el.pašto adresas";

            return View();
        }
    }
}