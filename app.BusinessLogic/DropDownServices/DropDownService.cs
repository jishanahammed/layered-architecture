using app.EntityModel.CoreModel;
using app.Infrastructure;
using app.Infrastructure.Auth;
using app.Services.Product_Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.DropDownServices
{
    public class DropDownService : IDropDownService
    {
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;
        public DropDownService(inventoryDbContext dbContext, IWorkContext workContext)
        {
            this.dbContext = dbContext;
            this.workContext = workContext;
        }
        public async Task<IEnumerable<DropDownViewmodel>> Companlist()
        {
            var user = await workContext.GetCurrentUserAsync();
            IEnumerable<DropDownViewmodel> dropDownViewmodels = await Task.Run(() => (from t1 in dbContext.Company
                                                                                      select new DropDownViewmodel
                                                                                      {
                                                                                          Id = t1.Id,
                                                                                          Name = t1.Name
                                                                                      }).AsQueryable());
            return dropDownViewmodels;
        }
    

        public async Task<IEnumerable<DropDownViewmodel>> productlist()
        {
            var user = await workContext.GetCurrentUserAsync();
            IEnumerable<DropDownViewmodel> dropDownViewmodels = await Task.Run(() => (from t11 in dbContext.UserProduct
                                                      join t1 in dbContext.Product on t11.ProductId equals t1.Id
                                                      join t2 in dbContext.ProductCategory on t1.ProductCategoryId equals t2.Id
                                                      join t3 in dbContext.ProductSubCategory on t1.ProductSubCategoryId equals t3.Id
                                                      join t4 in dbContext.Company on t11.CompanyId equals t4.Id
                                                      where t11.IsActive == true&&t1.TrakingId==user.Id
                                                      select new DropDownViewmodel
                                                      {
                                                          Id = t1.Id,
                                                          Name=t4.Name+" -- "+ t2.Name+" - "+t3.Name+" - "+t1.ProductName+" ("+t1.UnitName+")",
                                                      }).AsQueryable());
            return dropDownViewmodels;
        }

        public async Task<Product> sigleproduct(long id)
        {
            Product product= await dbContext.Product.FindAsync(id);
            return product;   
        }

        public async Task<List<DropDownCustomViewModel>> companyproductlist(long id)
        {
            List<DropDownCustomViewModel> dropDownViewmodels = await Task.Run(() => (from t1 in dbContext.Product
                                                                                      join t2 in dbContext.ProductCategory on t1.ProductCategoryId equals t2.Id
                                                                                      join t3 in dbContext.ProductSubCategory on t1.ProductSubCategoryId equals t3.Id
                                                                                      where t1.IsActive == true && t1.CompanyId == id
                                                                                      select new DropDownCustomViewModel
                                                                                      {
                                                                                          Id = t1.Id,
                                                                                          Name = t2.Name + " - " + t3.Name + " - " + t1.ProductName + " (" + t1.UnitName + ")",
                                                                                      }).ToListAsync());
            foreach (var item in dropDownViewmodels)
            {
                var userproduct=  dbContext.UserProduct.FirstOrDefault(f=>f.ProductId==item.Id&&f.IsActive==true);
                if (userproduct != null)
                {
                    item.IsAssigen= true;
                }
                else
                {
                    item.IsAssigen= false;  
                }
            }
            return dropDownViewmodels;
        }

        public async Task<IEnumerable<DropDownViewmodel>> vendorlist(int vendortype)
        {
            var users = await workContext.GetCurrentUserAsync();
            IEnumerable<DropDownViewmodel> dropDownViewmodels  = (from t1 in dbContext.Vendor.Where(x => x.VendorType == vendortype&&x.TrakingId==users.Id)
                     where t1.IsActive 
                     select new DropDownViewmodel
                     {
                         Name = "[" + t1.Name + "] " + t1.Mobile,
                         Id = t1.Id
                     }).OrderBy(x => x.Name).AsQueryable();

            return dropDownViewmodels;
        }
    }
}
