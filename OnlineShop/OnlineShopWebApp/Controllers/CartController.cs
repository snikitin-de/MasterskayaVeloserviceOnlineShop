using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public CartController(CartService cartService, ProductService productService, UserService userService)
        {
            _cartService = cartService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            var cartDb = await _cartService.GetAsync(userVM.Id);
            var item = Mapper.ToCartVM(cartDb);

            return View(item);
        }

        public async Task<EmptyResult> Add(string id)
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            await _cartService.AddProductAsync(userVM.Id, id);

            return new EmptyResult();
        }

        public async Task IncreaseCartItem(string productId)
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            await _cartService.IncreaseCartItemAsync(userVM.Id, productId);
        }

        public async Task DecreaseCartItem(string productId)
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            await _cartService.DecreaseCartItemAsync(userVM.Id, productId);
        }

        public async Task Clear()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            await _cartService.RemoveAsync(userVM.Id);
        }
    }
}
