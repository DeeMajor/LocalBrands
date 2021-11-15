using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalBrands.Models.Viewmodels
{
    public class DriverViewModel
    {
        public List<Order> orders { get; set; }
        public List<Order_Item> ReturningiItems { get; set; }

    }
}