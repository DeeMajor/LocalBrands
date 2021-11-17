using LocalBrands.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("1765915575", QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);

            byte[] byteee = ImageToByte(qrCodeImage);
            ViewBag.Message = byteee;

            return View();
        }
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}