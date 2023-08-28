using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;
using app.Services.UserServices;
using app.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using app.Services.UserpermissionsService;

namespace app.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices UserServices;
        private readonly IUserpermissionServices userpermission;
        private SignInManager<ApplicationUser> signInManager;
        public AccountController(IUserServices UserServices,
            IUserpermissionServices userpermission,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.signInManager = signInManager;
 
            this.UserServices = UserServices;
            this.userpermission = userpermission;
        }
        [HttpGet]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            model.UserName = "jishan.bd46@gmail.com";
            model.Password = "Ji123456";
            var  user =await UserServices.GetByUser(model.UserName);
            if (user == null)
            {
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                var getitem = await userpermission.GetAllMenuItemRecort(user.Id);
                HttpContext.Session.SetString("Username", model.UserName);
                var arry= JsonSerializer.Serialize(getitem);
                HttpContext.Session.SetString("ArrayData", arry);
                return Redirect("/Admin/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User is not verified");
                return View(model);
            }

        }

        public IActionResult AccessDenied()
        {
          
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
                HttpContext.Session.Clear();
                return Redirect("Login");
            }
            return Redirect("Login");
        }
    }
}
