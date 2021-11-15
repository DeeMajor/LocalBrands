using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LocalBrands.Models.Viewmodels
{
    public class BrandOwnerDashboardVM
    {
        //BrandOwner Details
        [DisplayName("Brand Owner Name")]
        public string BrandOwnerName { get; set; }
        [DisplayName("Surname")]
        public string BrandOwnerSurname { get; set; }
        [DisplayName("Email")]
        public string BrandOwnerEmail { get; set; }
        public string Status { get; set; }


        //Item Details belonging to the Owner

        public int NumberOfItems { get; set; }
        public int NumberOfCateories { get; set; }
        public int NumberOfDepartments { get; set; }
        public int NomberOFReturned { get; set; }
    }
}