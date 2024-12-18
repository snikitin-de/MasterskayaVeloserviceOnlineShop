using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    public class BikePartsController : Controller
    {
        private readonly BikePartsService _bikePartsService;
        private readonly ComparisonService _comparisonListService;
        private readonly FavoritesService _favoritesListService;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public BikePartsController(BikePartsService bikePartsService, ComparisonService comparisonListService, FavoritesService favoritesListService, ProductService productService, UserService userService)
        {
            _bikePartsService = bikePartsService;
            _comparisonListService = comparisonListService;
            _favoritesListService = favoritesListService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _bikePartsService.GetAllAsync();

            return View(items);
        }

        public async Task<IActionResult> AddProductToComparison(string id)
        {
            var user = await _userService.GetAuthorizedUserAsync();

            if (user != null)
            {
                await _comparisonListService.AddProductAsync(user.Id, id);

                return new EmptyResult();
            }

            return new UnauthorizedResult();
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
            var bikePartsDb = await _productService.SearchAsync(CategoryVM.BikeParts, text);
            var bikeParts = bikePartsDb.ToList();
            var mappedBikeParts = new List<BikePartVM>();

            foreach (var bikePart in bikeParts)
            {
                var item = Mapper.ProductVMToBikePartVM(bikePart);

                mappedBikeParts.Add(item);
            }

            if (mappedBikeParts.Count == 0)
            {
                return View("_EmptySearchPartial");
            }

            return View("index", mappedBikeParts);
        }
    }
}
