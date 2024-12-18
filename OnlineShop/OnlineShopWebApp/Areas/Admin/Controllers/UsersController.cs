using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public UsersController(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _userService.GetAllAsync();

            return View(items);
        }

        public async Task<IActionResult> User(string login)
        {
            var item = await _userService.GetAsync(login);

            return View("User", item);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddUserPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserVM user)
        {
            if (await _userService.IsExistsAsync(user))
            {
                ModelState.AddModelError("", "Такой логин уже существует");
            }

            if (ModelState.IsValid)
            {
                await _userService.AddAsync(user);

                return new EmptyResult();
            }

            return PartialView("_AddUserPartial");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string login)
        {
            var item = await _userService.GetAsync(login);

            return PartialView("_EditUserPartial", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserVM user)
        {
            await _userService.UpdateAsync(user);

            return new EmptyResult();
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string login)
        {
            var item = await _userService.GetAsync(login);

            return PartialView("_ChangePasswordPartial", item);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserVM userVM)
        {
            await _userService.ChangePasswordAsync(userVM);

            return RedirectToAction("index", "users");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(string login)
        {
            var user = await _userService.GetAsync(login);
            var roles = await _roleService.GetAllAsync();

            var userRolesViewModel = new UserRolesVM(user, roles);

            return PartialView("_ChangeRolePartial", userRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(UserVM user, string roleName)
        {
            await _userService.ChangeRoleAsync(user, roleName);

            var item = await _userService.GetAsync(user.Login);

            return View("User", item);
        }

        public async Task<IActionResult> Remove(UserVM user)
        {
            await _userService.RemoveAsync(user);

            return RedirectToAction("index", "users");
        }
    }
}
