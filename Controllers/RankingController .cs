using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TriviaGame.App.Services.interfaces;
using TriviaGame.App.Models.DTOs.Game;

namespace TriviaGame.App.Controllers
{
    public class RankingController : Controller
    {
        private readonly ILogger<RankingController> _logger;
        private readonly IGameApiService _gameApiService;

        public RankingController(ILogger<RankingController> logger, IGameApiService gameApiService)
        {
            _logger = logger;
            _gameApiService = gameApiService;
        }

        // GET: /Ranking/TablaPosiciones?categoryId=1
        public async Task<IActionResult> TablaPosiciones(int categoryId = 1, int top = 10)
        {
            var ranking = await _gameApiService.GetCategoryRankingAsync(categoryId, top);

            ViewBag.CategoryId = categoryId;
            ViewBag.Top = top;

            return View(ranking);
        }
    }
}
