using app.Services.Voucher_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.Report_service
{
    public interface IReportService
    {
        Task<VoucherViewModel> Generalledger(long vendorId);
    }
}
