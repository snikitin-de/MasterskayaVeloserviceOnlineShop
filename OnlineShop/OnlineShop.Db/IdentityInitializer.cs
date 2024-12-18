using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShop.SecretManager.Models;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationSecrets secrets)
        {
            if (await roleManager.FindByNameAsync(Constants.AdminRoleName) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = Constants.AdminRoleName, CanBeEdited = false, CanBeDeleted = false });
            }

            if (await roleManager.FindByNameAsync(Constants.UserRoleName) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = Constants.UserRoleName, CanBeEdited = false, CanBeDeleted = false });
            }

            if (await userManager.FindByNameAsync(secrets.DefaultAdminEmail) == null)
            {
                var admin = new User { Email = secrets.DefaultAdminEmail, UserName = secrets.DefaultAdminEmail };
                var avatar = new Image { Path = "/images/avatars/no-avatar.svg" };

                admin.Avatar = avatar;

                var result = await userManager.CreateAsync(admin, secrets.DefaultAdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Constants.AdminRoleName);
                }
            }
        }
    }
}
