using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocalBrands.Models
{
    public class Department
    {

        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Department_ID { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        //[Index("Department_Index", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(80)]
        public string Department_Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}