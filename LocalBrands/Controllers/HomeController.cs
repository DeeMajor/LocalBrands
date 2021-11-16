using LocalBrands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalBrands.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("Dashboard", "BrandOwners");
            }
            else if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Dashboard", "Admin");
            }else if (User.IsInRole("Driver"))
            {
                return RedirectToAction("Dashboard", "Drivers");
            }
            else 
            {
                return RedirectToAction("Index", "Shopping");
            }
          
        }
  
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}