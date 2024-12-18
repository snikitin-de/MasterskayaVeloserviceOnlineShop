using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var servicesDb = await _productService.GetAllServicesAsync();
            var bikePartsDb = await _productService.GetAllBikePartsAsync();
            var products = new List<ProductVM>();

            foreach (var serviceDb in servicesDb)
            {
                var service = Mapper.ToServiceVM(serviceDb);

                products.Add(service);
            }

            foreach (var bikePartDb in bikePartsDb)
            {
                var bikePart = Mapper.ToBikePartVM(bikePartDb);

                products.Add(bikePart);
            }

            return View(products);
        }

        #region Service

        [HttpPost]
        public async Task<IActionResult> AddService(ServiceVM service)
        {
            var images = await _productService.SaveImagesAsync(service.UploadedFiles, OnlineShopWebApp.Models.CategoryVM.Services);

            service.Images = images;

            var product = Mapper.ToService(service);

            await _productService.AddServiceAsync(product);

            return RedirectToAction("index", "products");
        }

        [HttpGet]
        public async Task<IActionResult> EditService(string id)
        {
            var serviceDb = await _productService.GetServiceAsync(id);
            var service = Mapper.ToServiceVM(serviceDb);

            return PartialView("Services/_EditServicePartial", service);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceVM service)
        {
            var images = await _productService.SaveImagesAsync(service.UploadedFiles, OnlineShopWebApp.Models.CategoryVM.Services);

            service.Images = images;

            var serviceDb = Mapper.ToService(service);

            await _productService.UpdateServiceAsync(serviceDb);

            return RedirectToAction("index", "products");
        }

        public async Task<IActionResult> ShowService(string id)
        {
            var serviceDb = await _productService.GetServiceAsync(id);
            var service = Mapper.ToServiceVM(serviceDb);

            return PartialView("Services/_ShowServicePartial", service);
        }

        public async Task<IActionResult> RemoveService(string id)
        {
            await _productService.RemoveServiceAsync(id);

            return RedirectToAction("index", "products");
        }

        #endregion Service

        #region BikeParts

        [HttpPost]
        public async Task<IActionResult> AddBikePart(BikePartVM bikePart)
        {
            var images = await _productService.SaveImagesAsync(bikePart.UploadedFiles, OnlineShopWebApp.Models.CategoryVM.BikeParts);

            bikePart.Images = images;

            var item = Mapper.ToBikePart(bikePart);

            await _productService.AddBikePartAsync(item);

            return RedirectToAction("index", "products");
        }

        [HttpGet]
        public async Task<IActionResult> EditBikePart(string id)
        {
            var bikePartDb = await _productService.GetBikePartAsync(id);
            var bikePart = Mapper.ToBikePartVM(bikePartDb);

            return PartialView("BikeParts/_EditBikePartPartial", bikePart);
        }

        [HttpPost]
        public async Task<IActionResult> EditBikePart(BikePartVM bikePart)
        {
            var images = await _productService.SaveImagesAsync(bikePart.UploadedFiles, OnlineShopWebApp.Models.CategoryVM.BikeParts);

            bikePart.Images = images;

            var bikePartDb = Mapper.ToBikePart(bikePart);

            await _productService.UpdateBikePartAsync(bikePartDb);

            return RedirectToAction("index", "products");
        }

        public async Task<IActionResult> ShowBikePart(string id)
        {
            var bikePartDb = await _productService.GetBikePartAsync(id);
            var bikePart = Mapper.ToBikePartVM(bikePartDb);

            return PartialView("BikeParts/_ShowBikePartPartial", bikePart);
        }

        public async Task<IActionResult> RemoveBikePart(string id)
        {
            await _productService.RemoveBikePartAsync(id);

            return RedirectToAction("index", "products");
        }

        #endregion BikeParts
    }
}
