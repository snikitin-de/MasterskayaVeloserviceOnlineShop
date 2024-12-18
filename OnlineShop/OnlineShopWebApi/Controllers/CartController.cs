using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Services;
using System.Security.Claims;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : ControllerBase
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

        [HttpGet(nameof(Get))]
        public async Task<Cart> Get()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetAsync(email);
            var cart = await _cartService.GetAsync(user.Id);

            return cart;
        }

        [HttpPost(nameof(AddProduct))]
        public async Task<IActionResult> AddProduct(string id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetAsync(email);
            var result = await _cartService.AddProductAsync(user.Id, id);

            if (result)
            {
                return Ok(new { Message = $"Товар {id} успешно добавлен в корзину {user.Id}" });
            }

            return BadRequest($"Возникла ошибка при добавлении товара {id} в корзину {user.Id}");
        }

        [HttpPatch(nameof(IncreaseCartItem))]
        public async Task<IActionResult> IncreaseCartItem(string productId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetAsync(email);
            var result = await _cartService.IncreaseCartItemAsync(user.Id, productId);

            if (result)
            {
                return Ok(new { Message = $"Количество товара {productId} увеличено на 1 в корзине {user.Id}" });
            }

            return BadRequest($"Возникла ошибка при увеличении количества товара {productId} на 1 в корзине {user.Id}");
        }

        [HttpPatch(nameof(DecreaseCartItem))]
        public async Task<IActionResult> DecreaseCartItem(string productId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetAsync(email);
            var result = await _cartService.DecreaseCartItemAsync(user.Id, productId);

            if (result)
            {
                return Ok(new { Message = $"Количество товара {productId} уменьшено на 1 в корзине {user.Id}" });
            }

            return BadRequest($"Возникла ошибка при уменьшении количества товара {productId} на 1 в корзине {user.Id}");
        }

        [HttpDelete(nameof(Clear))]
        public async Task<IActionResult> Clear()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetAsync(email);
            var result = await _cartService.RemoveAsync(user.Id);

            if (result)
            {
                return Ok(new { Message = $"Корзина {user.Id} успешно очищена" });
            }

            return BadRequest($"Возникла ошибка при очистке корзины {user.Id}");
        }
    }
}
