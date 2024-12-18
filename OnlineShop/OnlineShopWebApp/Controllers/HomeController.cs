using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;
using System.Diagnostics;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServicesService _servicesService;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, ServicesService servicesService, ProductService productService)
        {
            _logger = logger;
            _servicesService = servicesService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _servicesService.GetAllAsync();

            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Search(string text)
        {
            var servicesDb = await _productService.SearchAsync(CategoryVM.Services, text);
            var services = servicesDb.ToList();
            var mappedServices = new List<ServiceVM>();

            foreach (var service in services)
            {
                var item = Mapper.ProductVMToServiceVM(service);

                mappedServices.Add(item);
            }

            if (services.Count == 0)
            {
                return View("_EmptySearchPartial");
            }

            return View("index", mappedServices);
        }
    }
}
