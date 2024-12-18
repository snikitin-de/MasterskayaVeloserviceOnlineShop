using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly FavoritesService _favoritesService;
        private readonly UserService _userService;

        public FavoritesController(FavoritesService favoritesService, UserService userService)
        {
            _favoritesService = favoritesService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAuthorizedUserAsync();
            var favoritesDb = await _favoritesService.GetAsync(user.Id);
            var favorites = Mapper.ToFavoritesVM(favoritesDb);

            return View(favorites);
        }

        public async Task Clear()
        {
            var user = await _userService.GetAuthorizedUserAsync();
            await _favoritesService.RemoveAsync(user.Id);
        }

        public async Task<EmptyResult> Remove(string id)
        {
            var user = await _userService.GetAuthorizedUserAsync();
            await _favoritesService.RemoveProduct(user.Id, id);

            return new EmptyResult();
        }
    }
}
