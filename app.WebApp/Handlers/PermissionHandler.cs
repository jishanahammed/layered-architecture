using app.Services.UserpermissionsServices;
using app.WebApp.Handlers;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace WebApp.Handlers
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {

        public AuthorizationRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; }
    }

    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAssigeMenus assigeMenus;

        public PermissionHandler(IHttpContextAccessor httpContextAccessor, IAssigeMenus assigeMenus)
        {
            _httpContextAccessor = httpContextAccessor;
            this.assigeMenus = assigeMenus;
        }
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext && httpContext.GetEndpoint() is RouteEndpoint endpoint)
            {
                endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
                endpoint.RoutePattern.RequiredValues.TryGetValue("action", out var _action);
                endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
                endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);
                var serializedArrayFromSession = _httpContextAccessor.HttpContext.Session.GetString("ArrayData");
                MenuPermissionViewModel retrievedArray = new MenuPermissionViewModel();
                if (serializedArrayFromSession != null)
                {
                     retrievedArray = JsonSerializer.Deserialize<MenuPermissionViewModel>(serializedArrayFromSession);
                    // Now 'retrievedArray' contains the array of strings
                }
                List<MenuItemVM> menuItemVMs = new List<MenuItemVM>();
                foreach (var item in retrievedArray.MainMenuVM)
                {
                    foreach (var item1 in item.menuItemVMs)
                    {
                        MenuItemVM menu = new MenuItemVM();
                        menu.Name = item1.Name; 
                        menu.Controller = item1.Controller;
                        menu.Action = item1.Action;
                        menuItemVMs.Add(menu);  
                    }
                }

                var checkparmition = menuItemVMs.FirstOrDefault(s => s.Action == (string)_action && s.Controller == (string)_controller);
                //Check if a parent action is permitted then it'll allow child without checking for child permissions
                if (!string.IsNullOrWhiteSpace(requirement?.PermissionName) && !requirement.PermissionName.Equals("kgecomAuthorizatio"))
                {
                    _action = requirement.PermissionName;
                }
                if (checkparmition != null)
                {
                    if (requirement != null && context.User.Identity?.IsAuthenticated == true && _controller != null && _action != null && checkparmition.Action != null && checkparmition.Controller != null)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}
