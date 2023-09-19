using app.Services.Product_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.UserProduct_Service
{
    public class UserProductServiceViewModel:BaseViewModel
    {
        public long ProductId { set; get; }
        public string ProductName { set; get; }
        public long CompanyId { set; get; }
        public string CompanyName { set; get; }
        public decimal UnitPrice { set; get; }
        public decimal AVGPrice { set; get; }
        public decimal MRP { set; get; }
        public string ProductCategoryName { get; set; }
        public string ProductSubCategoryName { get; set; }
        public IEnumerable<UserProductServiceViewModel> ProductList { get; set; }
    }
}
