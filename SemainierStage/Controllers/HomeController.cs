using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemainierStage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index()
        //{
        //    if (User.IsInRole("Admin"))
        //    {
        //        return RedirectToAction("Index", "Etudiants");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Taches");
        //    }
        //}
    }
}