using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CartService _cartService;
        private readonly UserService _userService;

        public OrderController(OrderService orderService, CartService cartService, UserService userService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();

            if (userVM == null)
            {
                return View();
            }

            return View(Mapper.UserVMToOrderVM(userVM));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderVM order)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetAuthorizedUserAsync();
                var orderResult = await _orderService.AddAsync(user.Id, Mapper.ToOrder(order));
                await _cartService.RemoveAsync(user.Id);

                if (orderResult)
                {
                    return View("CreatedSuccess");
                }

                return View("CreatedFailure");
            }

            return RedirectToAction("index");
        }
    }
}
