using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;

namespace OnlineShopWebApp.Services
{
    public class ComparisonService
    {
        private readonly ProductService _productService;
        private readonly IComparisonRepository _comparisonRepository;

        public ComparisonService(ProductService productService, IComparisonRepository comparison)
        {
            _productService = productService;
            _comparisonRepository = comparison;
        }

        public async Task<Comparison> GetAsync(string id)
        {
            return await _comparisonRepository.GetAsync(id);
        }

        public async Task AddProductAsync(string userId, string id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product != null)
            {
                var comparisonDb = await GetAsync(userId);

                comparisonDb ??= await _comparisonRepository.AddAsync(userId);

                var newProduct = product;

                newProduct.Comparison = [comparisonDb];

                comparisonDb?.Products?.Add(product);

                var isAdded = await _comparisonRepository.AddAsync(comparisonDb);

                if (!isAdded)
                {
                    await _comparisonRepository.UpdateAsync(comparisonDb);
                }
            }
        }

        public async Task RemoveAsync(string userId)
        {
            await _comparisonRepository.RemoveAsync(userId);
        }
    }
}
