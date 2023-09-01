using app.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.ProductCategory_Services
{
    public class ProductCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string Remarks { get; set; }
        public int OrderNo { get; set; }
        public IEnumerable<ProductCategoryViewModel> ProductCategoriesList { get; set; }
    }
}
