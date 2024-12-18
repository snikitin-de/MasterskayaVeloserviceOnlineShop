using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly CartService _cartService;
        private readonly UserService _userService;

        public CartViewComponent(CartService cartService, UserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            var productsCount = 0m;

            if (userVM != null)
            {
                var cartDb = await _cartService.GetAsync(userVM.Id);
                var cart = Mapper.ToCartVM(cartDb);
                productsCount = cart.Quantity;
            }

            return View("Cart", productsCount);
        }
    }
}
