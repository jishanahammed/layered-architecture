using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.Voucher_Service
{
    public interface IVoucherService
    {
        Task<long> AddPurchaseVoucher(VoucherViewModel voucher);
    }
}
