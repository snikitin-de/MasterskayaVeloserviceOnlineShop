using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApi.Models;

namespace OnlineShopWebApi.Services
{
    public class AccountService : IAccountService
    {
        private SignInManager<User> _signInManager { get; }
        private UserManager<User> _userManager { get; }

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> HasAuthenticationPassedAsync(Authorization data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Login, data.Password, true, false);

            return result.Succeeded;
        }

        public async Task<bool> HasRegistrationPassedAsync(Registration data)
        {
            var user = new User { UserName = data.Email, Email = data.Email };
            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.UserRoleName);
            }

            return result.Succeeded;
        }
    }
}
