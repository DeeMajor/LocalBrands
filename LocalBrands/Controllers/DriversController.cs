using LocalBrands.Models;
using LocalBrands.Models.Viewmodels;
using LocalBrands.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            
            DriverViewModel driverViewModel = new DriverViewModel();
            var driverId = User.Identity.GetUserId();
            var AssignedOrders = db.Orders.ToList().Where(x => x.Driver_ID == driverId);
            var ReturnItems = db.Order_Items.Where(x => x.Return == true && x.Order.Driver_ID == driverId);

            driverViewModel.orders = AssignedOrders.ToList();
            driverViewModel.ReturningiItems.ToList();

            return View(driverViewModel);
        }
        public ActionResult Deliver(string id)
        {

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
            var driverId = User.Identity.GetUserId();
            var AssignedOrders = db.Orders.ToList().Where(x => x.Driver_ID == driverId);

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