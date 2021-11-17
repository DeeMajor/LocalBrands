using LocalBrands.Models;
using LocalBrands.Models.Viewmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalBrands.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BrandOwners
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DriverAssign(string id, string id2)
        {

            if (id != null && id2 != null)
            {
                Driver driver = db.Drivers.Find(id);
                Order order = db.Orders.Find(id2);

                order.Driver_ID = driver.DriverID;
                driver.Busy = true;
                db.Entry(order).State = EntityState.Modified;
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
        public ActionResult Accept(int id)
        {
            Order orderItem = db.Orders.Find(id);
            orderItem.status= "Return";
            db.Entry(orderItem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            AdminDashViewModel adminDash = new AdminDashViewModel();
          
            adminDash.Item = db.Items.ToList();
          adminDash.orders = db.Orders.ToList();
            adminDash.Order_Items = db.Order_Items.ToList();
            adminDash.Drivers = db.Drivers.ToList();
            adminDash.BrandOwners = db.BrandOwners.ToList();


            return View(adminDash);
        }
        [ChildActionOnly]
        public ActionResult RenderOrders()
        {
           
            var items = db.Orders.ToList();

            ViewBag.Order = new SelectList(db.Drivers, "DriverId", "Name");
            return PartialView("_Orders", items);
        }
        [ChildActionOnly]
        public ActionResult RendeItems()
        {
           
            var Categories = db.Items.ToList();
            return PartialView("_Items", Categories);
        }
        [ChildActionOnly]
        public ActionResult RenderBrandOwners()
        {
            
            var Departments = db.BrandOwners.ToList();
            return PartialView("_BrandOwners", Departments);
        }

        public ActionResult RenderReturned()
        {
            
            var items = db.Orders.Where(x=>x.status=="Return").ToList();

            return PartialView("_Returned", items);
        }

        public ActionResult RenderCreateDepartment()
        {


            return PartialView("_CreateDepartments");
        }
        public ActionResult RenderCreateCategory()
        {
            ViewBag.Department_ID = new SelectList(db.Departments, "Department_ID", "Department_Name");

            return PartialView("_CreateCategory");
        }
        public ActionResult RenderCreateItem()
        {
            ViewBag.Category_ID = new SelectList(db.Categories, "Category_ID", "Name");

            return PartialView("_CreateItem");
        }
        // GET: BrandOwners/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandOwner brandOwner = db.BrandOwners.Find(id);
            if (brandOwner == null)
            {
                return HttpNotFound();
            }
            return View(brandOwner);
        }

    }
}