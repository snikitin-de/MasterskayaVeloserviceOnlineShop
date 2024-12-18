using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private readonly RoleService _roleService;

        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _roleService.GetAllAsync();

            items = items.OrderBy(x => x.CanBeEdited && x.CanBeDeleted).ThenBy(x => x.Name).ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddRolePartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleVM roleVM)
        {
            if (await _roleService.IsExistsAsync(roleVM))
            {
                ModelState.AddModelError("", "Такая роль уже существует");
            }

            if (ModelState.IsValid)
            {
                await _roleService.AddAsync(roleVM);

                return new EmptyResult();
            }

            return PartialView("_AddRolePartial");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleService.GetByIdAsync(id);

            return PartialView("_EditRolePartial", role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleVM role)
        {
            await _roleService.UpdateAsync(role);

            return RedirectToAction("index", "roles");
        }

        public async Task<IActionResult> Remove(RoleVM role)
        {
            await _roleService.RemoveAsync(role);

            return RedirectToAction("index", "roles");
        }
    }
}
