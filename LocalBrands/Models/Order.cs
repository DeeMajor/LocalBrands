using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Order_ID { get; set; }
        public DateTime date_created { get; set; }
        [ForeignKey("Customer")]
        public string Email { get; set; }
        public bool shipped { get; set; }
        public string status { get; set; }
        public bool packed { get; set; }

        public virtual ICollection<Order_Item> Order_Items { get; set; }
        public virtual ICollection<Shipping_Address> Shipping_Addresses { get; set; }
        public virtual Customer Customer { get; set; }
        public string ConfirmationCode { get; set; }
        [ForeignKey("Driver")]
        public string Driver_ID { get; set; }
        [Display(Name = "QR Code")]
        public byte[] QrCodeImage { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}