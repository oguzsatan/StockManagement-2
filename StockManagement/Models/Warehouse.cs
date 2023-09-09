using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Warehouse : ModelBase
    {
        [Key]
        [DisplayName("Warehouse ID")]
        public int WarehouseID { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Availability")]
        public bool AvailableForTransfers { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Column(TypeName = "decimal(18,4)")]
        [DisplayName("Storage Space")]
        public decimal StorageSpace { get; set; }


        public List<Stock> Stocks { get; set; }
    }
}
