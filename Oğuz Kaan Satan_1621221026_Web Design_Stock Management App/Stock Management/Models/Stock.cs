using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class Stock : ModelBase
    {
        [Key]
        [DisplayName("Stock ID")]
        public int StockID { get; set; }


        [DisplayName("Warehouse")]
        public int WarehouseID { get; set; }


        public Warehouse Warehouse { get; set; }


        [DisplayName("Product")]
        public int ProductID { get; set; }


        public Product Product { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Total Count")]
        public int TotalCount { get; set; }

        public List<StockTransfer> FromStockTransfers { get; set; }

        public List<StockTransfer> ToStockTransfers { get; set; }

    }
}
