using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
