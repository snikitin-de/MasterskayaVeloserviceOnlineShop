using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public class ProductService
    {
        private readonly IProductsRepository _productRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private const string ProductImagesPath = @"/images/products";

        public ProductService(IProductsRepository productRepository, IWebHostEnvironment appEnvironment)
        {
            _productRepository = productRepository;
            _appEnvironment = appEnvironment;
        }

        public async Task<List<Image>> SaveImagesAsync(IEnumerable<IFormFile> uploadedFiles, CategoryVM category)
        {
            var images = new List<Image>();

            foreach (var uploadedFile in uploadedFiles)
            {
                if (uploadedFile != null)
                {
                    var relativePath = $@"{ProductImagesPath}/{category.ToString().ToLower()}/";
                    var productImagePath = Path.Combine(_appEnvironment.WebRootPath + relativePath);

                    if (!Directory.Exists(productImagePath))
                    {
                        Directory.CreateDirectory(productImagePath);
                    }

                    var fileName = Guid.NewGuid() + "." + uploadedFile.FileName.Split('.').Last();

                    using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    images.Add(new Image { Path = relativePath + fileName });
                }
                else
                {
                    images.Add(new Image { Path = @"/images/no-image-1920x1080.svg" });
                }
            }

            return images;
        }

        public async Task<List<ProductVM>> SearchAsync(CategoryVM category, string text)
        {
            var services = await _productRepository.GetAllServicesAsync();
            var bikeParts = await _productRepository.GetAllBikePartsAsync();

            if (text == null)
            {
                return [];
            }

            var searchedServicesDb = services.Where(x => x.Name.ToLower().Contains(text.ToLower()) && x.Category == (OnlineShop.Db.Models.Category)category).ToList();
            var searchedBikePartsDb = bikeParts.Where(x => x.Name.ToLower().Contains(text.ToLower()) && x.Category == (OnlineShop.Db.Models.Category)category).ToList();
            var searchedItems = new List<ProductVM>();

            if (category == CategoryVM.Services)
            {
                foreach (var searchedItemDb in searchedServicesDb)
                {
                    var searchedItem = Mapper.ServiceToProductVM(searchedItemDb);

                    searchedItems.Add(searchedItem);
                }
            }

            if (category == CategoryVM.BikeParts)
            {
                foreach (var searchedItemDb in searchedBikePartsDb)
                {
                    var searchedItem = Mapper.BikePartToProductVM(searchedItemDb);

                    searchedItems.Add(searchedItem);
                }
            }

            return searchedItems;
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _productRepository.GetAllServicesAsync();
        }

        public async Task<Service?> GetServiceAsync(string id)
        {
            if (id != null)
            {
                var product = await _productRepository.GetServiceAsync(id);

                if (product != null)
                {
                    return product;
                }
            }

            return null;
        }

        public async Task AddServiceAsync(Service service)
        {
            await _productRepository.AddServiceAsync(service);
        }

        public async Task UpdateServiceAsync(Service service)
        {
            await _productRepository.UpdateServiceAsync(service);
        }

        public async Task RemoveServiceAsync(string id)
        {
            await _productRepository.RemoveServiceAsync(id);
        }

        public async Task<List<BikePart>> GetAllBikePartsAsync()
        {
            return await _productRepository.GetAllBikePartsAsync();
        }

        public async Task<BikePart?> GetBikePartAsync(string id)
        {
            if (id != null)
            {
                var bikePart = await _productRepository.GetBikePartAsync(id);

                if (bikePart != null)
                {
                    return bikePart;
                }
            }

            return null;
        }

        public async Task AddBikePartAsync(BikePart BikePart)
        {
            await _productRepository.AddBikePartAsync(BikePart);
        }

        public async Task UpdateBikePartAsync(BikePart BikePart)
        {
            await _productRepository.UpdateBikePartAsync(BikePart);
        }

        public async Task RemoveBikePartAsync(string id)
        {
            await _productRepository.RemoveBikePartAsync(id);
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            if (id != null)
            {
                var product = await _productRepository.GetProductAsync(id);

                if (product != null)
                {
                    return product;
                }
            }

            return null;
        }
    }
}