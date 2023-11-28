using app.Services.Company_Service;
using app.Services.DropDownServices;
using app.Services.MainMenuService;
using app.Services.MenuItemService;
using app.Services.RolesServises;
using app.Services.UserpermissionsService;
using app.Services.UserServices;
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
            services.AddTransient<IDropDownService, DropDownService>();
            services.AddTransient<ICompanyService, CompanyServices>();
            return services;
        }
    }
}
