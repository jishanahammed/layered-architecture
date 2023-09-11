using app.Infrastructure.Auth;
using app.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Infrastructure.Repository;
using app.EntityModel.CoreModel;
using Microsoft.EntityFrameworkCore;

namespace app.Services.PurchaseFinalized_Services
{
    public class PurchaseFinalizedServices : IPurchaseFinalizedServices
    {
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;
        public PurchaseFinalizedServices(inventoryDbContext dbContext, IWorkContext workContext)
        {
            this.dbContext = dbContext;
            this.workContext = workContext;
        }
        public async Task<long> GetPurchaseFinalizedAsync(long id)
        {
            if (id == 0) { return -1; }
            PurchaseOrder purchase = await dbContext.PurchaseOrder.FindAsync(id);
            List<PurchaseOrderDetails> details = await dbContext.PurchaseOrderDetails.Where(d => d.PurchaseOrderId == purchase.Id).ToListAsync();
            if (details.Count == 0) { return -2; }
            using (var scope = dbContext.Database.BeginTransaction())
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
