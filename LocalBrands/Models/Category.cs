using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Department")]
        [Display(Name = "Brand")]
        public int Department_ID { get; set; }
        public virtual Department Department { get; set; }
        public string CreateBy { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}