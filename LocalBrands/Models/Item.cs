using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCode { get; set; }
        [Required]
        [ForeignKey("Category")]
        [DisplayName("Category*")]
        public int Category_ID { get; set; }

        [Required]
        [DisplayName("Name*")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Description*")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        [DisplayName("Quantitiy in Stock*")]
        public int QuantityInStock { get; set; }
        //[Required]
        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }
        [Required]
        [DisplayName("Price*")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int ReviewId { get; set; }
        public string CreateBy { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Cart_Item> Cart_Items { get; set; }
    }
}