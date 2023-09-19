using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Infrastructure;
using app.Utility.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.EntityModel.DatabaseView;

namespace app.Services.Stock_Service
{
    public class StockService : IStockService
    {
        private readonly IEntityRepository<StockInfo> _entityRepository;
        private readonly inventoryDbContext dbContext;
        private readonly IWorkContext workContext;
        public StockService(IEntityRepository<StockInfo> entityRepository, inventoryDbContext dbContext, IWorkContext workContext)
        {
            _entityRepository = entityRepository;
            this.dbContext = dbContext;
            this.workContext = workContext;
        }
        public Task<PagedModel<StockViewModel>> GetPagedListAsync(int page, int pageSize, DateTime fromdate, DateTime todate, string sarchString)
        {

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StockView>> GetStocks()
        {
            var user = await workContext.GetCurrentUserAsync();
            IQueryable<StockView> list =  dbContext.StockView.Where(d => d.TrakingId == user.Id).AsQueryable();
            return list;   
        }
    }
}
