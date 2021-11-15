using LocalBrands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gent_Coders.Services
{
    public class Payment_Service
    {
        private ApplicationDbContext ModelsContext;
        public Payment_Service()
        {
            this.ModelsContext = new ApplicationDbContext();
        }
        public List<Payment> GetPayments()
        {
            return ModelsContext.Payments.ToList();
        }
        public Payment GetOrderPayment(string order_Id)
        {
            return GetPayments().FirstOrDefault(x => x.Order_ID == order_Id);
        }
    }
}