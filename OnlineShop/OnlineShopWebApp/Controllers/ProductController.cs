using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string id, CategoryVM category)
        {
            if (category == CategoryVM.Services)
            {
                var serviceDb = await _productService.GetServiceAsync(id);
                var productViewModel = Mapper.ServiceToProductVM(serviceDb);

                return View(productViewModel);
            }

            if (category == CategoryVM.BikeParts)
            {
                var bikePartDb = await _productService.GetBikePartAsync(id);
                var productViewModel = Mapper.BikePartToProductVM(bikePartDb);

                return View(productViewModel);
            }

            return View();
        }
    }
}
