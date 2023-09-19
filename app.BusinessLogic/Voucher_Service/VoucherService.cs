using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Services.Vendor_Service;
using Microsoft.EntityFrameworkCore;

namespace app.Services.Voucher_Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IEntityRepository<Voucher> _entityRepository;
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;
        public VoucherService(IEntityRepository<Voucher> entityRepository, inventoryDbContext dbContext, IWorkContext workContext)
        {
            _entityRepository = entityRepository;
            this.dbContext = dbContext;
            this.workContext = workContext;
        }
        public async Task<long> AddPurchaseVoucher(VoucherViewModel voucher)
        {

            throw new NotImplementedException();
        }

        public async Task<VoucherViewModel> DetailsVoucher(long id)
        {
            VoucherViewModel model = new VoucherViewModel();
            var user = await workContext.GetCurrentUserAsync();
            model = await Task.Run(() => (from t1 in dbContext.Voucher
                                          join t3 in dbContext.Vendor on t1.VendorId equals t3.Id
                                          where t1.Id == id && t1.IsActive == true && t1.TrakingId == user.Id
                                          select new VoucherViewModel
                                          {
                                              Id = t1.Id,
                                              VoucherNo = t1.VoucherNo,
                                              VoucherDate = t1.VoucherDate,
                                              VoucherTypeId = t1.VoucherTypeId,
                                              VendorId = t1.VendorId,
                                              VendorName = t3.Name,
                                              VendorMobile = t3.Mobile,
                                              VendorEmail = t3.Email,
                                              VendorAddress = t3.Address,
                                              CreatedBy = t1.CreatedBy,
                                              CreatedOn = t1.CreatedOn,

                                          }).FirstOrDefaultAsync());

            model.voucherDetalisViewModels = await Task.Run(() => (from t1 in dbContext.Voucher
                                                                   join t2 in dbContext.VoucherDetails on t1.Id equals t2.VoucherId
                                                                   join t3 in dbContext.Product on t2.ProductId equals t3.Id
                                                                   where t1.Id == id && t1.IsActive == true && t1.TrakingId == user.Id
                                                                   select new VoucherDetalisViewModel
                                                                   {
                                                                       Id = t2.Id,
                                                                       VoucherId = t2.VoucherId,
                                                                       CreditAmount = t2.CreditAmount,
                                                                       DebitAmount = t2.DebitAmount,

                                                                   }).ToListAsync());
            return model;
        }
    }
}
