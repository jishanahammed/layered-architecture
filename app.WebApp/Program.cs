using app.Infrastructure;
using app.Infrastructure.Auth;
using app.Infrastructure.Repository;
using app.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace app.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // var config = new  IConfiguration();

            builder.Services.AddServiceModel(builder.Configuration);
            builder.Services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            builder.Services.AddTransient<IWorkContext, WorkContextsService>();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<inventoryDbContext>(options =>
            options.UseSqlServer(connectionString));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<inventoryDbContext>().AddDefaultTokenProviders();



            //builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.ConfigureApplicationCookie(
            options =>
            {
                options.LoginPath = new PathString("/Home/Index");
                options.AccessDeniedPath = new PathString("/Home/AccessDenied");
                options.LogoutPath = new PathString("/Home/Logout");
                options.Cookie.Name = "My.Cookie";
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromDays(30);
            });
     

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}