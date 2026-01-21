using Microsoft.AspNetCore.Mvc;
using TriviaGame.App.Services.interfaces;

namespace TriviaGame.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;

        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            // Lee el token de sesión
            var jwtToken = HttpContext.Session.GetString("JWT");

            var categories = await _categoryApiService.GetAllCategoriesAsync(jwtToken);
            return View(categories);
        }
    }
}
