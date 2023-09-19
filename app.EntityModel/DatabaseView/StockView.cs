using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.EntityModel.DatabaseView
{
    [Table("StockView")]
    public class StockView
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string TrakingId { get; set; }
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal AVGPrice { get; set; }

    }
}
