using app.EntityModel.CoreModel;
using app.Infrastructure;
using app.Infrastructure.Repository;
namespace app.Services.MainMenuService
{
    public class MainMenuServices : IMainMenuService
    {
        private readonly IEntityRepository<MainMenu> _entityRepository;
        public MainMenuServices( IEntityRepository<MainMenu> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public async Task<bool> AddRecort(MainMenuViewModel model)
        {
            var getitem =  _entityRepository.AllIQueryableAsync().FirstOrDefault(f => f.Name == model.Name && f.IsActive == true);
            if (getitem == null)
            {
                MainMenu menu=new MainMenu();
                menu.Name = model.Name;
                menu.Icon=model.Icon;
                menu.OrderNo = model.OrderNo;
                var result=await _entityRepository.AddAsync(menu);
                if (result.Id > 0)
                {
                 return true;
                }
                else
                {
                    return false;
                }               
            }
            return false;
        }

        public async Task<bool> DeleteRecort(long Id)
        {
            var getitem = await _entityRepository.GetByIdAsync(Id);
            if (getitem != null)
            {
                getitem.IsActive = false;
                var result = await _entityRepository.UpdateAsync(getitem);
                if (result)
                {
                return  true;
                }
                return false;
            }
            return false;
        }

        public async Task<List<MainMenu>> GetAllRecort()
        {
            List<MainMenu> getitem = _entityRepository.AllIQueryableAsync().OrderBy(d=>d.OrderNo).ToList();
            return getitem;
        }

        public async Task<bool> UpdateRecort(MainMenuViewModel model)
        {
            var menu = _entityRepository.AllIQueryableAsync().FirstOrDefault(f => f.Name == model.Name && f.IsActive == true&&f.Id!=model.Id);
            if (menu != null)
            {
                menu.Name = model.Name;
                menu.Icon = model.Icon;
                menu.OrderNo = model.OrderNo;
                var result = await _entityRepository.UpdateAsync(menu);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
