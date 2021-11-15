using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocalBrands.Models;

namespace LocalBrands.Controllers
{
    public class Order_ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order_Item
        public ActionResult Index()
        {
            var order_Items = db.Order_Items.Include(o => o.Item).Include(o => o.Order);
            return View(order_Items.ToList());
        }

        // GET: Order_Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            return View(order_Item);
        }

        // GET: Order_Item/Create
        public ActionResult Create()
        {
            ViewBag.item_id = new SelectList(db.Items, "ItemCode", "Name");
            ViewBag.Order_id = new SelectList(db.Orders, "Order_ID", "Email");
            return View();
        }

        // POST: Order_Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Item_id,Order_id,item_id,accepted,date_accepted,shipped,date_shipped,Return,ReturnReason")] Order_Item order_Item)
        {
            if (ModelState.IsValid)
            {
                db.Order_Items.Add(order_Item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.item_id = new SelectList(db.Items, "ItemCode", "Name", order_Item.item_id);
            ViewBag.Order_id = new SelectList(db.Orders, "Order_ID", "Email", order_Item.Order_id);
            return View(order_Item);
        }

        // GET: Order_Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_id = new SelectList(db.Items, "ItemCode", "Name", order_Item.item_id);
            ViewBag.Order_id = new SelectList(db.Orders, "Order_ID", "Email", order_Item.Order_id);
            return View(order_Item);
        }

        // POST: Order_Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Item_id,Order_id,item_id,accepted,date_accepted,shipped,date_shipped,Return,ReturnReason")] Order_Item order_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.item_id = new SelectList(db.Items, "ItemCode", "Name", order_Item.item_id);
            ViewBag.Order_id = new SelectList(db.Orders, "Order_ID", "Email", order_Item.Order_id);
            return View(order_Item);
        }

        // GET: Order_Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Item order_Item = db.Order_Items.Find(id);
            if (order_Item == null)
            {
                return HttpNotFound();
            }
            return View(order_Item);
        }

        // POST: Order_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Item order_Item = db.Order_Items.Find(id);
            db.Order_Items.Remove(order_Item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
