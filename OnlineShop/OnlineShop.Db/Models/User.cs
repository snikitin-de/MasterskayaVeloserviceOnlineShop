using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
        public string? Address { get; set; }
        public Image? Avatar { get; set; } = new Image { Path = Constants.NoAvatarImagePath };
    }
}
