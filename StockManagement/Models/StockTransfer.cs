using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class StockTransfer : ModelBase
    {
        public enum StockTransferType
        {
            Transfer,    // 0
            Inwards,     // 1
            Outwards,    // 2
        }

        [Key]
        [DisplayName("Operation ID")]
        public int StockTransferID { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Product ID")]
        public int ProductID { get; set; }


        public Product Product { get; set; }


        [DisplayName("From")]
        public int? FromStockID { get; set; }


        public Stock FromStock { get; set; }


        [DisplayName("To")]
        public int? ToStockID { get; set; }


        public Stock ToStock { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public int Count { get; set; }


        public StockTransferType Type { get; set; }
    }
}
