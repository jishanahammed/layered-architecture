using app.Services.UserpermissionsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace app.WebApp.AdminControllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("Username");
            var serializedArrayFromSession = HttpContext.Session.GetString("ArrayData");
            if (serializedArrayFromSession != null)
            {
                var retrievedArray = JsonSerializer.Deserialize<MenuPermissionViewModel>(serializedArrayFromSession);
                // Now 'retrievedArray' contains the array of strings
            }
            return View();
        }
    }
}
