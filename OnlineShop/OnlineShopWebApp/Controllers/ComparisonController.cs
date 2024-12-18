using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Services;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class ComparisonController : Controller
    {
        private readonly ComparisonService _comparisonService;
        private readonly UserService _userService;

        public ComparisonController(ComparisonService comparisonService, UserService userService)
        {
            _comparisonService = comparisonService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAuthorizedUserAsync();
            var comparisonDb = await _comparisonService.GetAsync(user.Id);
            var comparison = Mapper.ToComparisonVM(comparisonDb);

            return View(comparison);
        }

        public async Task Clear()
        {
            var user = await _userService.GetAuthorizedUserAsync();
            await _comparisonService.RemoveAsync(user.Id);
        }
    }
}
