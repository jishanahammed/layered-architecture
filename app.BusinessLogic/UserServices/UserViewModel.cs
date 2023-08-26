using app.Services.MenuItemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.UserServices
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string Prefix { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<UserViewModel> datalist { get; set; }
    }
}
