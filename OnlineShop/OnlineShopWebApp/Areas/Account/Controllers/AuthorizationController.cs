using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Authentication.Models;

namespace OnlineShopWebApp.Areas.Authentication.Controllers
{
    [Area("account")]
    public class AuthorizationController : Controller
    {
        private SignInManager<User> _signInManager { get; }

        public AuthorizationController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl, bool isModal)
        {
            if (isModal)
            {
                return PartialView("_AuthorizationPartial", new Authorization() { Login = "", Password = "" });
            }

            return View("Unauthorized", new Authorization() { Login = "", Password = "", AuthReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Authorization authorization, bool isModal)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.ShouldRememberMe, false);

                if (result.Succeeded)
                {
                    return Redirect(authorization.AuthReturnUrl);
                }

                ModelState.AddModelError("", "Неправильный логин или пароль");
            }

            if (isModal)
            {
                return PartialView("_AuthorizationPartial", authorization);
            }

            return View("Unauthorized", authorization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
