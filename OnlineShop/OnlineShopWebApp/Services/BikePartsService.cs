using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public class BikePartsService
    {
        private readonly ProductService _productService;

        public BikePartsService(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<BikePartVM>> GetAllAsync()
        {
            var bikePartsDb = await _productService.GetAllBikePartsAsync();
            var bikeParts = new List<BikePartVM>();

            foreach (var bikePartDb in bikePartsDb)
            {
                var bikePart = Mapper.ToBikePartVM(bikePartDb);

                bikeParts.Add(bikePart);
            }

            return bikeParts.Where(x => x.Category == CategoryVM.BikeParts).ToList();
        }
    }
}
