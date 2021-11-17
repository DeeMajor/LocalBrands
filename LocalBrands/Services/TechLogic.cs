using LocalBrands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace Gent_Coders.Services
{
    public class TechLogic
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();

        //List<OrderDetails> orderDetails = new List<OrderDetails>(); SSSS

        public TechLogic()
        {
            _db = new ApplicationDbContext();
        }

        public static Shipping_Address ShippingAddress(string id)
        {
            Shipping_Address address = _db.Shipping_Addresses.Where(x => x.Order_ID == id).FirstOrDefault();
            return address;
        }
    }
}