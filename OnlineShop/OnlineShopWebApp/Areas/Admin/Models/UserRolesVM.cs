using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class UserRolesVM
    {
        public UserVM User { get; set; }
        public List<RoleVM> Roles { get; set; }

        public UserRolesVM() { }

        public UserRolesVM(UserVM user, List<RoleVM> roles)
        {
            User = user;
            Roles = roles;
        }
    }
}
