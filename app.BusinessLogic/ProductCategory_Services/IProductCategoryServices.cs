using app.Utility.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.ProductCategory_Services
{
    public interface IProductCategoryServices
    {
        Task<int> AddRecord( ProductCategoryViewModel model);
        Task<int> UpdateRecord( ProductCategoryViewModel model);
        Task<bool> DeleteRecord( long id );
        Task<ProductCategoryViewModel> GetRecord( int id );
        Task<PagedModel<ProductCategoryViewModel>> GetPagedListAsync(int page, int pageSize);
    }
}
