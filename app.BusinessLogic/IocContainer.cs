using app.Services.DropDownServices;
using app.Services.MainMenuService;
using app.Services.MenuItemService;
using app.Services.Product_Services;
using app.Services.ProductCategory_Services;
using app.Services.ProductSubCategory_Service;
using app.Services.PurchaseFinalized_Services;
using app.Services.PurchaseOrder_Services;
using app.Services.RolesServises;
using app.Services.Stock_Service;
using app.Services.UserpermissionsService;
using app.Services.UserProduct_Service;
using app.Services.UserServices;
using app.Services.Vendor_Service;
using app.Services.Voucher_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services
{
    public static class IocContainer
    {
        public static IServiceCollection AddServiceModel(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMainMenuService, MainMenuServices>();
            services.AddTransient<IMenuItemService, MenuItemServices>();
            services.AddTransient<IUserpermissionServices, UserpermissionServices>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserServices, UsersServices>();
            services.AddTransient<IProductCategoryServices, ProductCategoryServices>();
            services.AddTransient<IProductSubCategoryService, ProductSubCategoryService>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IVendorService, VendorServices>();
            services.AddTransient<IPurchaseOrderServices, PurchaseOrderServices>();
            services.AddTransient<IDropDownService, DropDownService>();
            services.AddTransient<IUserProductService, UserProductService>();
            services.AddTransient<IPurchaseFinalizedServices, PurchaseFinalizedServices>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<IVoucherService, VoucherService>();
            return services;
        }
    }
}
