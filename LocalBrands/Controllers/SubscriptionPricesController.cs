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
    public class SubscriptionPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubscriptionPrices
        public ActionResult Index()
        {
            return View(db.SubscriptionPrices.ToList());
        }

        // GET: SubscriptionPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPrice subscriptionPrice = db.SubscriptionPrices.Find(id);
            if (subscriptionPrice == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPrice);
        }

        // GET: SubscriptionPrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubscriptionPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubscriptionPriceId,Price")] SubscriptionPrice subscriptionPrice)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionPrices.Add(subscriptionPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscriptionPrice);
        }

        // GET: SubscriptionPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPrice subscriptionPrice = db.SubscriptionPrices.Find(id);
            if (subscriptionPrice == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPrice);
        }

        // POST: SubscriptionPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubscriptionPriceId,Price")] SubscriptionPrice subscriptionPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriptionPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriptionPrice);
        }

        // GET: SubscriptionPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionPrice subscriptionPrice = db.SubscriptionPrices.Find(id);
            if (subscriptionPrice == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionPrice);
        }

        // POST: SubscriptionPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubscriptionPrice subscriptionPrice = db.SubscriptionPrices.Find(id);
            db.SubscriptionPrices.Remove(subscriptionPrice);
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
