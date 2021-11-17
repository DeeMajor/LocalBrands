using LocalBrands.Models;
using LocalBrands.Services;
using Microsoft.AspNet.Identity;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalBrands.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Order_Service order_Service;

        public OrdersController()
        {
            this.order_Service = new Order_Service();
        }
        //Customer orders
        public ActionResult Customer_Orders(string id)
        {
            var userName = User.Identity.GetUserName();
            if (User.IsInRole("Customer"))
            {
                if (String.IsNullOrEmpty(id) || id == "all")
                {
                    ViewBag.Status = "All";
                    return View(order_Service.GetOrders().Where(x=>x.Email==userName));
                }
                else
                {
                    ViewBag.Status = id;
                    return View(order_Service.GetOrders(id).Where(x => x.Email == userName));
                }
            }
            else
            {
                if (String.IsNullOrEmpty(id) || id == "all")
                {
                    ViewBag.Status = "All";
                    return View(order_Service.GetOrders());
                }
                else
                {
                    ViewBag.Status = id;
                    return View(order_Service.GetOrders(id));
                }
            }

          
        }
        public ActionResult New_Orders(string id)
        {
            if (String.IsNullOrEmpty(id) || id == "all")
            {
                ViewBag.Status = "All";
                return View(order_Service.GetOrders());
            }
            else
            {
                ViewBag.Status = id;
                return View(order_Service.GetOrders(id));
            }
        }
        public ActionResult Return_items(string id)
        {
            List<Order_Item> orderItems = new List<Order_Item>();

            var order_Item = db.Order_Items.Where(x => x.Order_id == id && x.Return == false).ToList();
            foreach (var item in order_Item)
            {
                orderItems.Add(item);
            }
            return View(orderItems);
        }
        [HttpPost]
        public ActionResult Return_items(int OrderItemId, bool? ReturnReason, string Reason)
        {

            if (ReturnReason == true)
            {
                Order_Item order_Item = db.Order_Items.Find(OrderItemId);
                order_Item.Return = true;
                order_Item.ReturnReason = Reason;
                db.Entry(order_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Return_Items", new { id = order_Item.Order_id });
            }
            return View();
        }
        public ActionResult ReturnOrder()
        {
            var UserId = User.Identity.GetUserName();
            var orderId = db.Order_Items.Where(x => x.Return == true && x.Order.Customer.Email == UserId).FirstOrDefault().Order.Order_ID;
            if (orderId != null)
            {
                Order order = db.Orders.Find(orderId);
                order.status = "Return";
                order.Driver_ID=null;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return View(order);
            }
            return View();
        }

        public ActionResult GetQRCode(string id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);

            Order order = db.Orders.Find(id);
            order.QrCodeImage = ImageToByte(qrCodeImage);
            db.Entry(order).State =  EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Customer_Orders");
        }
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        //public ActionResult GetQRCode()
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
        //        using (Bitmap bitMap = qrCode.GetGraphic(20))
        //        {
        //            bitMap.Save(ms, ImageFormat.Png);
        //            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return View();
        //}
        public ActionResult ReturnComplete(string id)
        {
            Order order = db.Orders.Find(id);
            foreach (var item in order.Order_Items)
            {
                item.Return = false;
                item.ReturnReason = "";
            }
            order.status = "Returned";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Order_Details(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.GetOrder(id) != null)
                return View(order_Service.GetOrderDetail(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Order_Tracking(string id)
        {
            //if (id == null)
            //    return RedirectToAction("Bad_Request", "Error");
            //if (order_Service.GetOrder(id) != null)
            //{
            //    ViewBag.Order = order_Service.GetOrder(id);
            //    return View(order_Service.GetOrderTrackingReport(id));
            //}
            //else
            //    return RedirectToAction("Not_Found", "Error");
            return View();
        }

        public ActionResult Mark_As_Packed(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.GetOrder(id) != null)
            {
                order_Service.MarkOrderAsPacked(id);
                return RedirectToAction("Order_Details", new { id = id });
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult schedule_OrderDelivery(string id, DateTime date)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.GetOrder(id) != null)
            {
                order_Service.schedule_OrderDelivery(id, date);
                return RedirectToAction("Order_Details", new { id = id });
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        //account orders
        public ActionResult Order_History()
        {
            return View(order_Service.GetOrders().Where(x => x.Customer.Email == User.Identity.Name));
        }
    }
}