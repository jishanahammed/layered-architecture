using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Infrastructure;
using app.Services.Voucher_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app.Services.Report_service
{
    public class ReportService : IReportService
    {
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;

        public ReportService(inventoryDbContext dbContext, IWorkContext workContext)
        {
            this.dbContext = dbContext;
            this.workContext = workContext;
        }

        public async Task<VoucherViewModel> Generalledger(long vendorId)
        {
            VoucherViewModel model = new VoucherViewModel();    
            var user = await workContext.GetCurrentUserAsync();
            var vendor = dbContext.Vendor.FirstOrDefault(d => d.Id == vendorId);
            model.VendorId = vendorId;  
            model.VendorName = vendor.Name; 
            model.VendorMobile = vendor.Mobile; 
            model.VendorAddress = vendor.Address;   
            model.VendorEmail = vendor.Email;  
            model.UserAddress = user.Address;   
            model.UserEmail = user.Email;
            model.UserMobile = user.PhoneNumber;
            var voucherList = await dbContext.Voucher.Where(f => f.VendorId == vendorId && f.TrakingId == user.Id&&f.IsSubmitted==true&&f.IsActive==true).ToListAsync();
            List<VoucherViewModel> vms=new List<VoucherViewModel>();
            decimal sumAmount = 0;
            foreach (var voucher in voucherList)
            {
                VoucherViewModel voucherView = new VoucherViewModel();  
                voucherView.VendorId = vendorId;
                voucherView.VoucherNo = voucher.VoucherNo;  
                voucherView.VoucherDate = voucher.VoucherDate;
                voucherView.Narration = voucher.Narration;
                voucherView.PaymentMethod = voucher.PaymentMethod;
                voucherView.CreditAmount = dbContext.VoucherDetails.Where(d => d.VoucherId == voucher.Id && d.IsActive == true&&d.CreditAmount>0).Sum(d=>d.CreditAmount);
                voucherView.DebitAmount = dbContext.VoucherDetails.Where(d => d.VoucherId == voucher.Id && d.IsActive == true&&d.DebitAmount>0).Sum(d=>d.DebitAmount);
                sumAmount = sumAmount + voucherView.DebitAmount;
                sumAmount = sumAmount - voucherView.CreditAmount;
                voucherView.Blance = sumAmount;
                vms.Add(voucherView);
            }
            model.datalist = vms;   
            return model;   
        }
    }
}
