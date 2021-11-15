using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LocalBrands.Models;
using LocalBrands.Models.Helpers;
using LocalBrands.Models.Viewmodels;
using Microsoft.AspNet.Identity;
using PayFast;
using PayFast.AspNet;

namespace LocalBrands.Controllers
{
    public class BrandOwnersController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BrandOwners
        public ActionResult Index()
        {
            return View(db.BrandOwners.ToList());
        }

        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var userEmail = db.Users.Find(userId).Email;
            var items = db.Items.ToList().Where(x => x.CreateBy == userId);
            var Categories = db.Categories.ToList().Where(x => x.CreateBy == userId);
            var Departments = db.Departments.ToList().Where(x => x.CreatedBy == userId);
            var BrandOwner = db.BrandOwners.Where(x => x.BrandOwnerEmail == userEmail).FirstOrDefault();

            if (BrandOwner==null)
            {
                return Error();
            }

            BrandOwnerDashboardVM owner = new BrandOwnerDashboardVM();
            owner.BrandOwnerEmail = userEmail;
            owner.BrandOwnerName = BrandOwner.BrandOwnerName;
            owner.BrandOwnerSurname = BrandOwner.BrandOwnerSurname;
            owner.NomberOFReturned = 0;
            owner.NumberOfCateories = Categories.Count();
            owner.NumberOfDepartments = Departments.Count();
            owner.NumberOfItems = items.Count();
            owner.Status = BrandOwner.Status;

            return View(owner);
        }
        [ChildActionOnly]
        public ActionResult RenderItems()
        {
            var userId = User.Identity.GetUserId();
            var items = db.Items.ToList().Where(x => x.CreateBy == userId);
            return PartialView("_Items", items);
        }
        [ChildActionOnly]
        public ActionResult RenderCategories()
        {
            var userId = User.Identity.GetUserId();
            var Categories = db.Categories.ToList().Where(x => x.CreateBy == userId);
            return PartialView("_Categories", Categories);
        }
        [ChildActionOnly]
        public ActionResult RenderDepartments()
        {
            var userId = User.Identity.GetUserId();
            var Departments = db.Departments.ToList().Where(x => x.CreatedBy == userId);
            return PartialView("_Departments", Departments);
        }

        public ActionResult RenderReturned()
        {
            var userId = User.Identity.GetUserId();
            var items = db.Order_Items.ToList().Where(x => x.Item.CreateBy == userId);

            return PartialView("_Departments", items);
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

        // GET: BrandOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,BrandOwnerName,BrandOwnerSurname,BrandOwnerEmail,ContactNumber,Address,Status")] BrandOwner brandOwner)
        {
            if (ModelState.IsValid)
            {
                db.BrandOwners.Add(brandOwner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brandOwner);
        }

        // GET: BrandOwners/Edit/5
        public ActionResult Edit(string id)
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

        // POST: BrandOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,BrandOwnerName,BrandOwnerSurname,BrandOwnerEmail,ContactNumber,Address,Status")] BrandOwner brandOwner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brandOwner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brandOwner);
        }

        // GET: BrandOwners/Delete/5
        public ActionResult Delete(string id)
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

        // POST: BrandOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BrandOwner brandOwner = db.BrandOwners.Find(id);
            db.BrandOwners.Remove(brandOwner);
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
        public BrandOwnersController()
        {


            this.payFastSettings = new PayFastSettings();
            this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
            this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
            this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
            this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
            this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
            this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
            this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        }
        //Payment
        #region Fields

        private readonly PayFastSettings payFastSettings;

        #endregion Fields

        #region Constructor

        //public ApprovedOwnersController()
        //{

        //}

        #endregion Constructor

        #region Methods



        public ActionResult Recurring()
        {
            var recurringRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            recurringRequest.merchant_id = this.payFastSettings.MerchantId;
            recurringRequest.merchant_key = this.payFastSettings.MerchantKey;
            recurringRequest.return_url = this.payFastSettings.ReturnUrl;
            recurringRequest.cancel_url = this.payFastSettings.CancelUrl;
            recurringRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            recurringRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            recurringRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            recurringRequest.amount = 20;
            recurringRequest.item_name = "Recurring Option";
            recurringRequest.item_description = "Some details about the recurring option";

            // Transaction Options
            recurringRequest.email_confirmation = true;
            recurringRequest.confirmation_address = "drnendwandwe@gmail.com";

            // Recurring Billing Details
            recurringRequest.subscription_type = SubscriptionType.Subscription;
            recurringRequest.billing_date = DateTime.Now;
            recurringRequest.recurring_amount = 20;
            recurringRequest.frequency = BillingFrequency.Monthly;
            recurringRequest.cycles = 0;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{recurringRequest.ToString()}";

            return Redirect(redirectUrl);
        }
        public void ChangeStatu()
        {
            var userame = User.Identity.GetUserName();
            var dbRecord = db.BrandOwners.Where(x => x.BrandOwnerEmail == userame).FirstOrDefault();
            dbRecord.Status = BrandOwnerStatus.Paid;
            db.Entry(dbRecord).State = EntityState.Modified;
            db.SaveChanges();
        }
        public ActionResult OnceOff()
        {
          
            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
            ChangeStatu();
            // Merchant Details
            onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
            onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
            onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
            onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
            onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details

            onceOffRequest.email_address = "sbtu01@payfast.co.za";

            decimal amount = db.SubscriptionPrices.Select(p => p.Price).FirstOrDefault();
            // Transaction Details
            onceOffRequest.m_payment_id = "";
            onceOffRequest.amount = Convert.ToDouble(amount);
            onceOffRequest.item_name = "Subscription payment";
            onceOffRequest.item_description = "Some details about the once off payment";


            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult AdHoc()
        {
            var adHocRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            adHocRequest.merchant_id = this.payFastSettings.MerchantId;
            adHocRequest.merchant_key = this.payFastSettings.MerchantKey;
            adHocRequest.return_url = this.payFastSettings.ReturnUrl;
            adHocRequest.cancel_url = this.payFastSettings.CancelUrl;
            adHocRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            adHocRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            adHocRequest.m_payment_id = "";
            adHocRequest.amount = 70;
            adHocRequest.item_name = "Adhoc Agreement";
            adHocRequest.item_description = "Some details about the adhoc agreement";

            // Transaction Options
            adHocRequest.email_confirmation = true;
            adHocRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            adHocRequest.subscription_type = SubscriptionType.AdHoc;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{adHocRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult Return()
        {
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Notify([ModelBinder(typeof(PayFastNotifyModelBinder))] PayFastNotify payFastNotifyViewModel)
        {
            payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            System.Diagnostics.Debug.WriteLine($"Signature Validation Result: {isValid}");

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, IPAddress.Parse(this.HttpContext.Request.UserHostAddress));

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            System.Diagnostics.Debug.WriteLine($"Merchant Id Validation Result: {merchantIdValidationResult}");

            var ipAddressValidationResult = payfastValidator.ValidateSourceIp();

            System.Diagnostics.Debug.WriteLine($"Ip Address Validation Result: {merchantIdValidationResult}");

            // Currently seems that the data validation only works for successful payments
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                System.Diagnostics.Debug.WriteLine($"Data Validation Result: {dataValidationResult}");
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {
                System.Diagnostics.Debug.WriteLine($"Subscription was cancelled");
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Error()
        {
            return View();
        }

        #endregion Methods
    }
}
