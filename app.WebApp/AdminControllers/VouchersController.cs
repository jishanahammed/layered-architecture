using app.Services.PurchaseOrder_Services;
using app.Services.Voucher_Service;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{
    public class VouchersController : Controller
    {
        private readonly IVoucherService voucherService;
        public VouchersController(IVoucherService voucherService)
        {
            this.voucherService = voucherService;
        }

        public async Task<ActionResult> Index(int page = 1, int pagesize = 10, string sarchString = null)
        {
            if (page < 1)
                page = 1;
            var results = await voucherService.GetPagedListAsync(page, pagesize, sarchString);
            return View(results);
        }
        public async Task<ActionResult> GetPaged(int page = 1, int pagesize = 10, string sarchString = null)
        {
            if (page < 1)
                page = 1;
            var results = await voucherService.GetPagedListAsync(page, pagesize, sarchString);
            return PartialView("_Paymentvoucher", results);
        }
        public async Task<IActionResult> SupplierPayment()
        {
            var BnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime BaTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, BnTimeZone);
            VoucherViewModel viewModel = new VoucherViewModel();
            viewModel.VoucherDate = BaTime;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SupplierPayment(VoucherViewModel model)
        {
            var res= await voucherService.AddPurchaseVoucher(model);    
            return RedirectToAction("Supplier_Payment_Details", new {id= res });
        }
        public async Task<IActionResult> Supplier_Payment_Details(long id)
        {
            var result= await voucherService.DetailsVoucher(id);
            return View(result);
        }




    }
}
