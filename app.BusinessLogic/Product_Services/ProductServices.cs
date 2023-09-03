using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Infrastructure;
using app.Utility.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.Product_Services
{
    public class ProductServices : IProductServices
    {
        private readonly IEntityRepository<Product> _entityRepository;
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;
        public ProductServices(IEntityRepository<Product> entityRepository, inventoryDbContext dbContext, IWorkContext workContext)
        {
            _entityRepository = entityRepository;
            this.dbContext = dbContext;
            this.workContext = workContext;
        }
        public Task<int> AddRecord(ProductViewModel model)
        {
            var checkname = _entityRepository.AllIQueryableAsync().FirstOrDefault(f => f.ProductName.Trim() == model.ProductName.Trim());
            if (checkname == null)
            { 

            }
                throw new NotImplementedException();
        }

        public Task<bool> DeleteRecord(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedModel<ProductViewModel>> GetPagedListAsync(int page, int pageSize, int ProductCategoryId, int ProductSubCategoryId, string ProductType, string sarchString)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetRecord(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRecord(ProductViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
