using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private Guid _userId = Guid.Parse("a975b678-27f5-4144-be91-94f67bd6eaea");

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var itemsDb = await _orderService.GetAllAsync();
            var items = itemsDb.Select(Mapper.ToOrderVM).ToList();

            return View(items);
        }

        public async Task<IActionResult> Order(string id)
        {
            var itemDb = await _orderService.GetAsync(id);
            var item = Mapper.ToOrderVM(itemDb);

            return View("Order", item);
        }

        public async Task<IActionResult> ChangeStatus(string id, OrderStatusVM status)
        {
            await _orderService.ChangeStatusAsync(id, Mapper.ToOrderStatus(status));

            var itemDb = await _orderService.GetAsync(id);
            var item = Mapper.ToOrderVM(itemDb);

            return View("Order", item);
        }
    }
}
