using app.EntityModel.CoreModel;
using app.Infrastructure;
using app.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
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
    }
}
