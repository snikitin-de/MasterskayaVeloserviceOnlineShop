using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;

        public AccountController(UserService userService, OrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserVM user)
        {
            await _userService.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAvatar(string login)
        {
            var user = await _userService.GetAsync(login);

            return PartialView("_EditAvatarPartial", user);
        }

        [HttpPost]
        public async Task<IActionResult> EditAvatar(UserVM userVM)
        {
            if (userVM.UploadedFile != null)
            {
                var user = await _userService.GetAsync(userVM.Login);
                var avatar = await _userService.SaveAvatarAsync(userVM.UploadedFile);

                if (user.Avatar.Path != Constants.NoAvatarImagePath)
                {
                    user.Avatar.Path = avatar.Path;
                }
                else
                {
                    user.Avatar = avatar;
                }

                await _userService.UpdateAsync(user);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAvatar(string login)
        {
            var user = await _userService.GetAsync(login);

            user.Avatar.Path = Constants.NoAvatarImagePath;

            await _userService.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string login)
        {
            var user = await _userService.GetAsync(login);

            return PartialView("_ProfilePartial", user);
        }

        public async Task<IActionResult> Orders(string login)
        {
            var user = await _userService.GetAsync(login);
            var orders = new List<OrderVM>();

            if (user != null)
            {
                var itemsDb = await _orderService.GetAllAsync();
                var items = itemsDb.Where(x => x.UserId == user.Id).Select(Mapper.ToOrderVM).ToList();

                orders.AddRange(items);
            }

            return PartialView("_OrdersPartial", orders);
        }
    }
}
