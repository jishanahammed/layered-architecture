using app.EntityModel.CoreModel;
using app.Infrastructure.Repository;
using app.Infrastructure;
using app.Services.UserpermissionsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Services.MenuItemService;

namespace app.Services.UserpermissionsService
{
    public class UserpermissionServices : IUserpermissionServices
    {
        private readonly IEntityRepository<Userpermissions> _entityRepository;
        private readonly inventoryDbContext dbContext;
        public UserpermissionServices(IEntityRepository<Userpermissions> entityRepository, inventoryDbContext dbContext)
        {
            _entityRepository = entityRepository;
            this.dbContext = dbContext;
        }
        public async Task<bool> AddRecort(long Id, string UserId)
        {
            Userpermissions userpermissions = new Userpermissions();
            var result = await dbContext.Userpermissions.FirstOrDefaultAsync(d => d.MenuItem == Id && d.UserId == UserId);
            if (result == null) {
                userpermissions.UserId = UserId;
                userpermissions.MenuItem = Id;
                var res = await _entityRepository.AddAsync(userpermissions);
            }
            else
            {
                if (result.IsActive == true)
                {
                    result.IsActive = false;
                }
                else
                {
                    result.IsActive = true;
                }
                var res = await _entityRepository.UpdateAsync(result);
            }
            return true;
        }

        public async Task<UserpermissionViewModel> GetAllRecort(string Id)
        {
           UserpermissionViewModel viewModel=new UserpermissionViewModel();
            List<UserpermissionViewModel> models = new List<UserpermissionViewModel>(); 
            var result = await dbContext.MainMenu.Where(s => s.IsActive == true).ToListAsync();
            foreach (var item in result)
            {
                UserpermissionViewModel model = new UserpermissionViewModel();
                model.MenuName = item.Name;
                models.Add(model);
                var itemlist= dbContext.MenuItem.Where(d=>d.MenuId==item.Id).ToList();
                List<MenuItemViewModel> menuitemlist = new List<MenuItemViewModel>();
                foreach (var menu in itemlist)
                {
                    var res = await dbContext.Userpermissions.FirstOrDefaultAsync(d => d.MenuItem == menu.Id && d.UserId == Id);
                    MenuItemViewModel vm = new MenuItemViewModel();
                    vm.Id = menu.Id;
                    vm.Name = menu.Name;
                    if (res != null&&res.IsActive==true)
                    { vm.IsPermission = true; }

                    else { vm.IsPermission = false; }
                    menuitemlist.Add(vm);
                }
                model.menuitemlist = menuitemlist;
            }
            viewModel.datalist = models;
            return viewModel;
        }
    }
}
