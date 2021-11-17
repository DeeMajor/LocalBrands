using LocalBrands.Models;
using LocalBrands.Models.Viewmodels;
using LocalBrands.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalBrands.Controllers
{
    public class DriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Order_Service order_Service;
        public DriversController()
        {
            this.order_Service = new Order_Service();
        }
        // GET: Drivers
        public ActionResult Dashboard()
        {
            
            DriverViewModel driverViewModel = new DriverViewModel();
            var UserId = User.Identity.GetUserName();
            var AssignedOrders = db.Orders.Where(x => x.Driver.Email == UserId).ToList();

            if (db.Order_Items != null)
            {
                var ReturnItems = db.Order_Items.Where(x => x.Return == true && x.Order.Driver.Email == UserId).ToList();
                driverViewModel.ReturningiItems = ReturnItems ;
            }
            

                driverViewModel.orders = AssignedOrders.ToList();
        

               
            

            return View(driverViewModel);
        }
        public ActionResult Deliver(string id)
        {
            Session["OrderId"] = id;

            return View();
        }
        []
        public ActionResult Handover(string id)
        {
            Order order = db.Orders.Where(x => x.Order_ID == id).FirstOrDefault();
        

            return View(order);
        }
        [HttpPost]
        public ActionResult Success(string id)
        {

            Order order = db.Orders.Where(x => x.Order_ID == id).FirstOrDefault();
            order.status = "Delivered";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }
        public ActionResult OrderItems(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.GetOrder(id) != null)
                return View(order_Service.GetOrderDetail(id));
            else
                return RedirectToAction("Not_Found", "Error");
          
        }
        public ActionResult Complete(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
      
                Order order = db.Orders.Find(id);
            order.status = "Complete";
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult RenderOrders()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var UserId = User.Identity.GetUserName();
            var AssignedOrders = db.Orders.Where(x => x.Driver.Email == UserId).ToList();

        

            driverViewModel.orders = AssignedOrders.ToList();

            return PartialView("_Orders", driverViewModel);
        }
        [ChildActionOnly]
        public ActionResult RenderReturnItems()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var driverId = User.Identity.GetUserId();
            var ReturnItems = db.Order_Items.Where(x => x.Return == true && x.Order.Driver_ID == driverId);
            driverViewModel.ReturningiItems.ToList();

            return PartialView("_ReturnItems", driverViewModel);
        }
        [ChildActionOnly]
        public ActionResult RenderCompletedOrders()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            var driverId = User.Identity.GetUserId();
            var AssignedOrders = db.Orders.ToList().Where(x => x.Driver_ID == driverId && x.status=="Completed");

            driverViewModel.orders = AssignedOrders.ToList();

            return PartialView("_CompletedOrders", driverViewModel);
        }

    }
}