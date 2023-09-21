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
using app.Utility;
using app.Utility.Miscellaneous;
using app.Services.Product_Services;

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
        public async Task<long> AddPurchaseVoucher(VoucherViewModel model)
        {
            VoucherType voucherType = dbContext.VoucherType.Where(x => x.Code == "CRV").FirstOrDefault();
            var user = await workContext.GetCurrentUserAsync();
            using (var scope = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Voucher voucher = new Voucher();
                    voucher.VoucherDate = model.VoucherDate;
                    voucher.VoucherNo = GetVoucherNo(voucherType.Id, voucherType.Code, user.Id, model.VoucherDate);
                    voucher.VoucherTypeId = voucherType.Id;
                    voucher.VendorId = model.VendorId;
                    voucher.PaymentMethod =((PaymentMethod)model.SupplierPaymentMethodEnumFK).ToString();
                    voucher.ReferenceId = 0;
                    voucher.Narration = model.Narration;
                    voucher.CreatedBy = user.FullName;
                    voucher.TrakingId = user.Id;
                    voucher.CreatedOn = DateTime.Now;
                    voucher.IsActive = true;
                    voucher.IsSubmitted = true;
                    dbContext.Voucher.Add(voucher);
                    dbContext.SaveChanges();

                        VoucherDetails voucherDetails = new VoucherDetails();
                        voucherDetails.VoucherId = voucher.Id;
                        voucherDetails.ProductId = 0;
                        voucherDetails.ReferenceId = 0;
                        voucherDetails.DebitAmount = 0;
                        voucherDetails.CreditAmount = model.Amount;
                        voucherDetails.Titel = "";
                        voucherDetails.Particular = model.Narration;
                        voucherDetails.CreatedBy = user.FullName;
                        voucherDetails.TrakingId = user.Id;
                        voucherDetails.CreatedOn = DateTime.Now;
                        voucherDetails.IsActive = true;

                    dbContext.VoucherDetails.Add(voucherDetails);
                    dbContext.SaveChanges();
                    scope.Commit();
                    return voucher.Id;
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                    return 0;
                }
            }
        }
        public string GetVoucherNo(int voucherTypeId, string Code, string userid, DateTime voucherDate)
        {
            string voucherNo = string.Empty;
            int vouchersCount = dbContext.Voucher.Where(x => x.VoucherTypeId == voucherTypeId && x.VoucherDate.Month == voucherDate.Month
            && x.VoucherDate.Year == voucherDate.Year && x.TrakingId == userid).Count();
            vouchersCount++;
            voucherNo = Code + "-" + vouchersCount.ToString().PadLeft(4, '0');
            return voucherNo;
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
                                              Narration = t1.Narration,

                                          }).FirstOrDefaultAsync());

            model.voucherDetalisViewModels = await Task.Run(() => (from t1 in dbContext.Voucher
                                                                   join t2 in dbContext.VoucherDetails on t1.Id equals t2.VoucherId
                                                                   
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

        public async Task<IEnumerable<Voucher>> PaymentVoucherList()
        {
            var user = await workContext.GetCurrentUserAsync();
           IQueryable<Voucher> result =  dbContext.Voucher.Where(d=>d.VoucherTypeId==3&&d.IsActive==true&&d.TrakingId== user.Id).AsQueryable();
            return result;
        }


        public async Task<PagedModel<VoucherViewModel>> GetPagedListAsync(int page, int pageSize, string sarchString)
        {
            VoucherViewModel model = new VoucherViewModel();
            var user = await workContext.GetCurrentUserAsync();
            model.voucherlist = await Task.Run(() => (from t1 in dbContext.Voucher
                                          join t3 in dbContext.Vendor on t1.VendorId equals t3.Id
                                          where t1.VoucherTypeId== 3 && t1.IsActive == true && t1.TrakingId == user.Id
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
                                              Narration = t1.Narration,
                                          }).AsQueryable());
            int resCount = model.voucherlist.Count();
            var pagers = new PagedList(resCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var pagedList = model.voucherlist.Skip(recSkip).Take(pagers.PageSize).ToList();
            int FirstSerialNumber = ((page * pageSize) - pageSize) + 1;
            PagedModel<VoucherViewModel> pagedModel = new PagedModel<VoucherViewModel>()
            {
                Models = pagedList,
                FirstSerialNumber = FirstSerialNumber,
                PagedList = pagers,
                TotalEntity = resCount,
                UserType = user.UserType,
            };
            return pagedModel;
        }
    }
}
