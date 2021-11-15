using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class Driver
    {
        [Key]
        public string DriverID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool Busy { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}