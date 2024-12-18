using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public class ServicesService
    {
        private readonly ProductService _productService;

        public ServicesService(ProductService productDbService)
        {
            _productService = productDbService;
        }

        public async Task<List<ServiceVM>> GetAllAsync()
        {
            var servicesDb = await _productService.GetAllServicesAsync();
            var services = new List<ServiceVM>();

            foreach (var serviceDb in servicesDb)
            {
                var service = Mapper.ToServiceVM(serviceDb);

                services.Add(service);
            }

            return services.Where(x => x.Category == CategoryVM.Services).ToList();
        }
    }
}
