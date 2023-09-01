using app.EntityModel.CoreModel;
using app.Infrastructure.Repository;
using app.Infrastructure;
using app.Utility.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.ProductCategory_Services
{
    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IEntityRepository<ProductCategory> _entityRepository;
        private readonly inventoryDbContext dbContext;
        public ProductCategoryServices(IEntityRepository<ProductCategory> entityRepository, inventoryDbContext dbContext)
        {
            _entityRepository = entityRepository;
            this.dbContext = dbContext;
        }
        public async Task<int> AddRecord(ProductCategoryViewModel model)
        {
            var checkname = _entityRepository.AllIQueryableAsync().FirstOrDefault(f => f.Name.Trim() == model.Name.Trim());
            if (checkname == null)
            {
                ProductCategory category = new ProductCategory();
                category.Name = model.Name;
                var res = await _entityRepository.AddAsync(category);
                return 2;
            }
            return 1;
        }

        public async Task<bool> DeleteRecord(long id)
        {
            var result = await _entityRepository.GetByIdAsync(id);
            result.IsActive = false;
            await _entityRepository.UpdateAsync(result);
            return true;
        }

        public async Task<PagedModel<ProductCategoryViewModel>> GetPagedListAsync(int page, int pageSize)
        {
            ProductCategoryViewModel model = new ProductCategoryViewModel();
            model.ProductCategoriesList = await Task.Run(() => (from t1 in dbContext.ProductCategory
                                                                where t1.IsActive == true
                                                                select new ProductCategoryViewModel
                                                                {
                                                                    Id = t1.Id,
                                                                    ProductType = t1.ProductType,
                                                                    Name = t1.Name
                                                                }).AsEnumerable());
            int resCount = model.ProductCategoriesList.Count();
            var pagers = new PagedList(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var pagedList = model.ProductCategoriesList.Skip(recSkip).Take(pagers.PageSize).ToList();
            int FirstSerialNumber = ((page * pageSize) - pageSize) + 1;
            PagedModel<ProductCategoryViewModel> pagedModel = new PagedModel<ProductCategoryViewModel>()
            {
                Models = pagedList,
                FirstSerialNumber = FirstSerialNumber,
                PagedList = pagers,
                TotalEntity = resCount
            };
            return pagedModel;
        }

        public async Task<ProductCategoryViewModel> GetRecord(int id)
        {
            var result = await _entityRepository.GetByIdAsync(id);
            ProductCategoryViewModel model = new ProductCategoryViewModel();
            model.Id = result.Id;
            model.Name = result.Name;
            return model;
        }

        public async Task<int> UpdateRecord(ProductCategoryViewModel model)
        {
            var checkname = _entityRepository.AllIQueryableAsync().FirstOrDefault(f => f.Name.Trim() == model.Name.Trim() && f.Id != model.Id);
            if (checkname == null)
            {
                var result = await _entityRepository.GetByIdAsync(model.Id);
                result.Name = model.Name;
                await _entityRepository.UpdateAsync(result);
                return 2;
            }
            return 1;
        }
    }
}
