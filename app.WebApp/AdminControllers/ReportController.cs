using app.Services.Report_service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.WebApp.AdminControllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        public async Task<IActionResult> SupplierGeneralledger()
        {
            return View();
        }
        public async Task<IActionResult> SupplierGeneralledgerReport(long id)
        {
            var result = await reportService.Generalledger(id);
            return View(result);
        }

        public async Task<IActionResult> CustomerGeneralledger()
        {
            return View();
        }
        public async Task<IActionResult> CustomerGeneralledgerReport(long id)
        {
            var result = await reportService.Generalledger(id);
            return View(result);
        }
    }
}
