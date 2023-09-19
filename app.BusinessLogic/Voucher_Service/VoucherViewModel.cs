using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.Voucher_Service
{
    public class VoucherViewModel:BaseViewModel
    {
        public int VoucherTypeId { set; get; }
        public string VoucherTypeCode { set; get; }
        public string VoucherNo { set; get; }
        public DateTime VoucherDate { set; get; }
        public string Narration { set; get; }
        public long ReferenceId { set; get; }
        public string ReferenceNo { set; get; }
        public long VendorId { set; get; }
        public string VendorName { set; get; }
        public string VendorMobile { set; get; }
        public string VendorEmail { set; get; }
        public string VendorAddress { set; get; }
        public List<VoucherDetalisViewModel> voucherDetalisViewModels { set; get; }
    }
}
