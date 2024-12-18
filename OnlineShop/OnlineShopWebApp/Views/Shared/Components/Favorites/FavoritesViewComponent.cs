using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly FavoritesService _favoritesService;
        private readonly UserService _userService;

        public FavoritesViewComponent(FavoritesService favoritesService, UserService userService)
        {
            _favoritesService = favoritesService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            var productsCount = 0m;

            if (userVM != null)
            {
                var favoritesDb = await _favoritesService.GetAsync(userVM.Id);
                productsCount = favoritesDb.Products.Count();
            }

            return View("Favorites", productsCount);
        }
    }
}
