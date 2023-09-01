using app.EntityModel.CoreModel;
using app.Services.MainMenuService;
using app.Services.ProductCategory_Services;
using app.Utility.Miscellaneous;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{ 
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryServices productCategoryServices;
        public ProductCategoryController(IProductCategoryServices productCategoryServices)
        {
            this.productCategoryServices = productCategoryServices;
        }
        public async Task<ActionResult> Index(int page = 1, int pagesize = 10)
        {
            if (page < 1)
                page = 1;
            var results = await productCategoryServices.GetPagedListAsync(page, pagesize);
            return View(results);
        }
        public async Task<ActionResult> GetPaged(int page = 1, int pagesize = 10)
        {
            if (page < 1)
                page = 1;
            var results = await productCategoryServices.GetPagedListAsync(page, pagesize);
            return PartialView("_ProductCategorypartial", results);
        }

        [HttpGet]
        public async Task<IActionResult> AddRecort()
        {
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecort(ProductCategoryViewModel viewModel)
        {
            var result = await productCategoryServices.AddRecord(viewModel);
            if (result==2)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Same Name already exists!");
            return View(viewModel);
        }


    }
}
