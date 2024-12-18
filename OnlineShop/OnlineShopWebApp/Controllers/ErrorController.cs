using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";

            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }

            return View("PageNotFound");
        }
    }
}
