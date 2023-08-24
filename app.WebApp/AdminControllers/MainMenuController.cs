
using app.Services.MainMenuService;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{
    public class MainMenuController : Controller
    {
        private readonly IMainMenuService mainMenu;
        public MainMenuController(IMainMenuService mainMenu)
        {
            this.mainMenu = mainMenu;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            MainMenuViewModel mainMenuViewModel = new MainMenuViewModel();
            mainMenuViewModel.datalist =  await mainMenu.GetAllRecort();
            return View(mainMenuViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AddRecort()
        {
            MainMenuViewModel viewModel = new MainMenuViewModel();
            return View(viewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> AddRecort(MainMenuViewModel viewModel)
        {
            var result= await mainMenu.AddRecort(viewModel);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
