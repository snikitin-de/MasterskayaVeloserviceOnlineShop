using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Views.Shared.Components.Comparison
{
    public class ComparisonViewComponent : ViewComponent
    {
        private readonly ComparisonService _comparisonService;
        private readonly UserService _userService;

        public ComparisonViewComponent(ComparisonService comparisonService, UserService userService)
        {
            _comparisonService = comparisonService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userVM = await _userService.GetAuthorizedUserAsync();
            var productsCount = 0m;

            if (userVM != null)
            {
                var comparisonDb = await _comparisonService.GetAsync(userVM.Id);
                productsCount = comparisonDb.Products.Count();
            }

            return View("Comparison", productsCount);
        }
    }
}
