using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;

namespace OnlineShopWebApp.Services
{
    public class CartService
    {
        private readonly ProductService _productService;
        private readonly ICartsRepository _cartRepository;

        public CartService(ProductService productService, ICartsRepository cartRepository)
        {
            _productService = productService;
            _cartRepository = cartRepository;
        }

        public async Task<Cart?> GetAsync(string id)
        {
            return await _cartRepository.GetAsync(id);
        }

        public async Task<bool> AddProductAsync(string userId, string id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product != null)
            {
                var cartDb = await GetAsync(userId);

                if (cartDb != null)
                {
                    if (cartDb.Items.Any(x => x.Product.Id == product.Id))
                    {
                        var existingCartItem = cartDb.Items.First(x => x.Product.Id == product.Id);
                        cartDb.Increase(existingCartItem);
                        await _cartRepository.UpdateAsync(cartDb);

                        return true;
                    }
                }
                else
                {
                    cartDb = new Cart
                    {
                        UserId = userId
                    };
                }

                var cartItem = new CartItem()
                {
                    Product = product,
                    Cart = cartDb
                };

                cartDb?.Items?.Add(cartItem);

                var isAdded = await _cartRepository.AddAsync(cartDb);

                if (!isAdded)
                {
                    return await _cartRepository.UpdateAsync(cartDb);
                }

                return isAdded;
            }

            return false;
        }

        public async Task<bool> IncreaseCartItemAsync(string userId, string productId)
        {
            var cartDb = await GetAsync(userId);
            var cartItemDb = cartDb.Items.FirstOrDefault(x => x.Product.Id == productId);

            cartDb.Increase(cartItemDb);

            var result = await _cartRepository.UpdateAsync(cartDb);

            return result;
        }

        public async Task<bool> DecreaseCartItemAsync(string userId, string productId)
        {
            var cartDb = await GetAsync(userId);
            var cartItemDb = cartDb.Items.FirstOrDefault(x => x.Product.Id == productId);

            cartDb.Decrease(cartItemDb);

            var result = await _cartRepository.UpdateAsync(cartDb);

            return result;
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            try
            {
                await _cartRepository.RemoveAsync(userId);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
