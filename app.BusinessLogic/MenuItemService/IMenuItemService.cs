using app.EntityModel.CoreModel;
using app.Services.MainMenuService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.MenuItemService
{
    public interface IMenuItemService
    {
        Task<bool> AddRecord(MenuItemViewModel model);
        Task<MenuItemViewModel> GetAllRecord();
        Task<bool> UpdateRecord(MenuItemViewModel model);
        Task<bool> DeleteRecord(long Id);
        Task<MenuItemViewModel> GetByRecord(long Id);
    }
}
