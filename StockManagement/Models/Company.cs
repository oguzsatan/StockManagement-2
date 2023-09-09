using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Company : ModelBase //(inheritance) Model Base nin özelliklerini alıyor.
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Company")]
        public int CompanyID { get; set; }


        [Required(ErrorMessage = "Company must have a name")]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; } // Nautilus-Depo1


        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Is Active")]
        public bool IsActiveProvider { get; set; } //true


        public List<Product> Products { get; set; }
    }
}
