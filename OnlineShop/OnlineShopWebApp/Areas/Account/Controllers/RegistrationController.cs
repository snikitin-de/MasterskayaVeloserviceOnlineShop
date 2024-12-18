using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Authentication.Models;

namespace OnlineShopWebApp.Areas.Authentication.Controllers
{
    [Area("account")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private SignInManager<User> _signInManager { get; }

        public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_RegistrationPartial", new Registration() { Login = "", Password = "", ConfirmedPassword = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (registration.Login == registration.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }

            var user = new User { Email = registration.Login, UserName = registration.Login };

            if (await _userManager.FindByEmailAsync(registration.Login) != null)
            {
                ModelState.AddModelError("", "Такой логин уже существует");
            }

            if (ModelState.IsValid)
            {
                var addUserResult = await _userManager.CreateAsync(user, registration.Password);

                if (addUserResult.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, Constants.UserRoleName);

                    if (addRoleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                    }
                }
                else
                {
                    foreach (var error in addUserResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return new EmptyResult();
            }

            return PartialView("_RegistrationPartial", registration);
        }
    }
}
