using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Product : ModelBase
    {
        [Key]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Company")]
        public int CompanyID { get; set; }


        public Company Company { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Column(TypeName = "decimal(18,4)")]
        [DisplayName("Occupied Space")]
        public decimal SpaceOccupied { get; set; }


        public List<Stock> Stocks { get; set; }

        public List<StockTransfer> StockTransfers { get; set; }
    }
}
