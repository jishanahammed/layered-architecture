using app.EntityModel.CoreModel;
using app.Services.DropDownServices;
using app.Services.Product_Services;
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
        public async Task<ActionResult> Index(int page = 1, int pagesize = 10,string sarchString = null)
        {
            if (page < 1)
                page = 1;
            var results = await purchaseOrderServices.GetPagedListAsync(page, pagesize, sarchString);
            return View(results);
        }
        [HttpGet]
        public async Task<ActionResult> GetPaged(int page = 1, int pagesize = 10,string sarchString = null)
        {
            if (page < 1)
                page = 1;
            var results = await purchaseOrderServices.GetPagedListAsync(page, pagesize,sarchString);
            return PartialView("_PurchaseOrder", results);
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
            if (result>0)
            {
                return RedirectToAction("Detalis", new {id=result});
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detalis(long id)
        {
            var result = await purchaseOrderServices.GetPurchaseOrder(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> PurchaseOrderDetailReport(long id)
        {
            var result = await purchaseOrderServices.GetPurchaseOrder(id);
            return View(result);
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

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(long id)
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateRecord(long id)
        {
            var result = await purchaseOrderServices.GetPurchaseOrder(id);
            return View(result);
        }  
        
        [HttpGet]
        public async Task<IActionResult> DeletepurchaseOrderDetalis(long id)
        {
            var result = await purchaseOrderServices.DeletePurchaseOrderDetalies(id);
            return RedirectToAction("UpdateRecord", new { id = result });
        } 
        [HttpPost]
        public async Task<IActionResult> AddpurchaseOrderDetalis(PurchaseOrderDetailsViewModel model)
        {
            var result = await purchaseOrderServices.AddPurchaseOrderDetalies(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRecord(PurchaseOrderViewModel viewModel)
        {
            var result = await purchaseOrderServices.UpdatePurchaseOrder(viewModel);
            if (result > 0)
            {
                return RedirectToAction("Detalis", new { id = result });
            }

            return View(viewModel);
        }
    }
}
