using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class Role : IdentityRole
    {
        public bool CanBeDeleted { get; set; } = true;
        public bool CanBeEdited { get; set; } = true;

        public Role() { }
    }
}
