using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ServicesService _servicesService;
        private readonly FavoritesService _favoritesListService;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public ServicesController(ServicesService servicesService, FavoritesService favoritesListService, ProductService productService, UserService userService)
        {
            _servicesService = servicesService;
            _favoritesListService = favoritesListService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var servicesDb = await _servicesService.GetAllAsync();

            return View(servicesDb);
        }

        public async Task<IActionResult> AddProductToFavorites(string id)
        {
            var user = await _userService.GetAuthorizedUserAsync();

            if (user != null)
            {
                await _favoritesListService.AddProductAsync(user.Id, id);

                return new EmptyResult();
            }

            return new UnauthorizedResult();
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
