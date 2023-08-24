using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace app.Infrastructure.Auth
{
    public class WorkContextsService : IWorkContext
    {
        private const string UserGuidCookiesName = "TBDUser";

        private ApplicationUser _currentUser;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private HttpContext _httpContext;
        private readonly IConfiguration _configuration;

        public WorkContextsService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           IHttpContextAccessor contextAccessor,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = contextAccessor.HttpContext;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            var contextUser = _httpContext.User;
            _currentUser = await _userManager.GetUserAsync(contextUser);

            if (_currentUser != null)
            {
                return _currentUser;
            }

            var userGuid = GetUserGuidFromCookies();
            //if (userGuid.HasValue)
            //{
            //    _currentUser = _userRepository.Query().Include(x => x.Roles).FirstOrDefault(x => x.UserGuid == userGuid);
            //}

            if (_currentUser != null && await _userManager.IsInRoleAsync(_currentUser, "Admin"))
            {
                return _currentUser;
            }

            //userGuid = Guid.NewGuid();
            //var dummyEmail = string.Format("{0}@guest.ecommerce.com", userGuid);
            //_currentUser = new ApplicationUser
            //{
            //    FullName = userGuid.ToString(),
            //    Email = dummyEmail,
            //    UserName = dummyEmail,
            //};
            //var abc = await _userManager.CreateAsync(_currentUser, "User@123");
            //await _userManager.AddToRoleAsync(_currentUser, "Admin");
            //SetUserGuidCookies();
            return _currentUser;
        }
        public async Task<ApplicationUser> CurrentUserAsync()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            var contextUser = _httpContext.User;
            _currentUser = await _userManager.GetUserAsync(contextUser);

            if (_currentUser != null)
            {
                return _currentUser;
            }
            return _currentUser;
        }
        private Guid? GetUserGuidFromCookies()
        {
            if (_httpContext.Request.Cookies.ContainsKey(UserGuidCookiesName))
            {
                return Guid.Parse(_httpContext.Request.Cookies[UserGuidCookiesName]);
            }

            return null;
        }

        private void SetUserGuidCookies()
        {
            _httpContext.Response.Cookies.Append(UserGuidCookiesName, _currentUser.Id.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(5),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict
            });
        }

        public async Task<bool> IsUserSignedIn()
        {
            var contextUser = _httpContext.User;
            _currentUser = await _userManager.GetUserAsync(contextUser);

            if (_currentUser != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
