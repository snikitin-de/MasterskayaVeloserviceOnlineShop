using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;

namespace OnlineShopWebApp.Services
{
    public class FavoritesService
    {
        private readonly ProductService _productService;
        private readonly IFavoritesRepository _favoritesRepository;

        public FavoritesService(ProductService productService, IFavoritesRepository favoritesRepository)
        {
            _productService = productService;
            _favoritesRepository = favoritesRepository;
        }

        public async Task<Favorites> GetAsync(string id)
        {
            return await _favoritesRepository.GetAsync(id);
        }

        public async Task AddProductAsync(string userId, string id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product != null)
            {
                var favoritesDb = await GetAsync(userId);

                favoritesDb ??= await _favoritesRepository.AddAsync(userId);

                var newProduct = product;

                newProduct.Favorites = [favoritesDb];

                favoritesDb?.Products?.Add(product);

                var isAdded = await _favoritesRepository.AddAsync(favoritesDb);

                if (!isAdded)
                {
                    await _favoritesRepository.UpdateAsync(favoritesDb);
                }
            }
        }

        public async Task RemoveProduct(string userId, string id)
        {
            var favoritesDb = await _favoritesRepository.GetAsync(userId);

            if (favoritesDb.Products.Any(x => x.Id == id))
            {
                var item = favoritesDb.Products.First(x => x.Id == id);

                favoritesDb.Products.Remove(item);

                await _favoritesRepository.UpdateAsync(favoritesDb);
            }
        }

        public async Task RemoveAsync(string userId)
        {
            await _favoritesRepository.RemoveAsync(userId);
        }
    }
}
