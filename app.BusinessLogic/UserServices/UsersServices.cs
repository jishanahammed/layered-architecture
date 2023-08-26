using app.EntityModel.CoreModel;
using app.Infrastructure.Repository;
using app.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Services.MenuItemService;

namespace app.Services.UserServices
{
    public class UsersServices : IUserServices
    {
        private readonly inventoryDbContext dbContext;
        public UsersServices(inventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<UserViewModel> GetAllRecort()
        {
            UserViewModel model = new UserViewModel();
            model.datalist = await Task.Run(() => (from t1 in dbContext.Users                                                  
                                                   select new UserViewModel
                                                   {
                                                       UserId = t1.Id,
                                                       FullName = t1.FullName,
                                                       UserName = t1.UserName,                                                      
                                                       IsActive = t1.IsActive,
                                                       Email = t1.Email,
                                                       Mobile=t1.PhoneNumber,
                                                   }).OrderByDescending(x => x.UserName).AsEnumerable());
            return model;
        }
    }
}
