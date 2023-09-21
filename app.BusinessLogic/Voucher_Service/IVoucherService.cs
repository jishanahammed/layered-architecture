using app.EntityModel.CoreModel;
using app.Services.UserProduct_Service;
using app.Services.Vendor_Service;
using app.Utility.Miscellaneous;
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
        Task<VoucherViewModel> DetailsVoucher(long id);
        Task<IEnumerable<Voucher>> PaymentVoucherList();
        Task<PagedModel<VoucherViewModel>> GetPagedListAsync(int page, int pageSize, string sarchString);
    }
}
