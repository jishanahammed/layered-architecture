
using app.Services.MainMenuService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{
    [Authorize("kgecomAuthorizatio")]
    public class MainMenuController : Controller
    {
        private readonly IMainMenuService mainMenu;
        public MainMenuController(IMainMenuService mainMenu)
        {
            this.mainMenu = mainMenu;
        }

        [HttpGet]
    
        public async Task<IActionResult> Index()
        {
            MainMenuViewModel mainMenuViewModel = new MainMenuViewModel();
            mainMenuViewModel.datalist = await mainMenu.GetAllRecord();
            return View(mainMenuViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AddRecord()
        {
            MainMenuViewModel viewModel = new MainMenuViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecort(MainMenuViewModel viewModel)
        {
            var result = await mainMenu.AddRecord(viewModel);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Same Name already exists!");
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var res = await mainMenu.DeleteRecord(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRecort(long id)
        {
            MainMenuViewModel viewModel = new MainMenuViewModel();
            viewModel= await mainMenu.GetByRecord(id);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRecort(MainMenuViewModel viewModel)
        {
            var result = await mainMenu.UpdateRecord(viewModel);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Same Name already exists!");
            return View(viewModel);
        }

    }
}
