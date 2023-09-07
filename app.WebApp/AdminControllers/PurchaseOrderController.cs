using app.Services.DropDownServices;
using app.Services.ProductSubCategory_Service;
using app.Services.PurchaseOrder_Services;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{
    public class PurchaseOrderController : Controller
    {

        private readonly IPurchaseOrderServices purchaseOrderServices;
        private readonly IDropDownService dropDownService;
        public PurchaseOrderController(IPurchaseOrderServices purchaseOrderServices, IDropDownService dropDownService)
        {
            this.purchaseOrderServices = purchaseOrderServices;
            this.dropDownService = dropDownService; 
        }
        [HttpGet]
        public async Task<IActionResult> AddRecord()
        {
            var BnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime BaTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, BnTimeZone);
            PurchaseOrderViewModel viewModel = new PurchaseOrderViewModel();
            viewModel.PurchaseDate = BaTime;
            viewModel.DeliveryDate = BaTime;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord(PurchaseOrderViewModel viewModel)
        {
            var result = await purchaseOrderServices.AddPurchaseOrder(viewModel);
            if (result == 2)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Same Name already exists!");
            return View(viewModel);
        }
        [HttpGet]
        public async Task<JsonResult> GetAutoCompleteSupplierGet()
        {
            var products = await dropDownService.vendorlist(1);
            return Json(products);
        }

        [HttpGet]
        public async Task<JsonResult> GetAutoCompleteProductGet()
        {
            var products = await dropDownService.productlist();
            return Json(products);
        }
        [HttpGet]
        public async Task<JsonResult> ProductGet(long id)
        {
            var products = await dropDownService.sigleproduct(id);
            return Json(products);
        }

    }
}
