using app.EntityModel.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.MainMenuService
{
    public interface IMainMenuService
    {
        Task<bool> AddRecord(MainMenuViewModel model);
        Task<List<MainMenu>> GetAllRecord();
        Task<bool> UpdateRecord(MainMenuViewModel model);
        Task<bool> DeleteRecord(long Id);
        Task<MainMenuViewModel> GetByRecord(long Id);
    }
}
