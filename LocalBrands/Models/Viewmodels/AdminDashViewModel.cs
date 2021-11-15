using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalBrands.Models.Viewmodels
{
    public class AdminDashViewModel
    {
        public List<Order> orders { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<BrandOwner> BrandOwners { get; set; }
        public List<Order_Item> Order_Items { get; set; }
        public List<Item> Item { get; set; }
    }
}