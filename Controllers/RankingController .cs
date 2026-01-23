using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.Category;
using TriviaGame.App.Models.DTOs.Game;
using TriviaGame.App.Services.interfaces;

namespace TriviaGame.App.Controllers
{
    public class RankingController : Controller
    {
        private readonly ILogger<RankingController> _logger;
        private readonly IGameApiService _gameApiService;
        private readonly ICategoryApiService _categoryApiService;

        public RankingController(ILogger<RankingController> logger,
                                IGameApiService gameApiService,
                                ICategoryApiService categoryApiService)
        {
            _logger = logger;
            _gameApiService = gameApiService;
            _categoryApiService = categoryApiService;
        }

        // GET: /Ranking/TablaPosiciones?categoryId=1
        public async Task<IActionResult> TablaPosiciones(int categoryId = 1, int top = 10)
        {
            // Traer ranking
            var ranking = await _gameApiService.GetCategoryRankingAsync(categoryId, top);

            // Traer JWT de sesion
            var jwtToken = HttpContext.Session.GetString("JWT");

            // Traer categorias pasando el token
            var categories = await _categoryApiService.GetAllCategoriesAsync(jwtToken);

            ViewBag.CategoryId = categoryId;
            ViewBag.Top = top;
            ViewBag.Categories = categories; // categorias para el selector

            return View(ranking);
        }
    }
}