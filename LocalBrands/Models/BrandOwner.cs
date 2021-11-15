using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class BrandOwner
    {
        [Key]
        public string EmployeeId { get; set; }
        [DisplayName("Brand Owner Name")]
        public string BrandOwnerName { get; set; }
        [DisplayName("Surname")]
        public string BrandOwnerSurname { get; set; }
        [DisplayName("Email")]
        public string BrandOwnerEmail { get; set; }
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}