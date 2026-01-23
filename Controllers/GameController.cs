using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TriviaGame.App.Services.interfaces;
using TriviaGame.App.Models.DTOs.Game;
using Microsoft.AspNetCore.Http;

namespace TriviaGame.App.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameApiService _gameApiService;

        public GameController(ILogger<GameController> logger, IGameApiService gameApiService)
        {
            _logger = logger;
            _gameApiService = gameApiService;
        }

    [HttpGet]
       [HttpGet]
public IActionResult Index(int gameSessionId)
{
    if (gameSessionId <= 0)
        return NotFound();

    ViewBag.GameSessionId = gameSessionId;
    return View();
}

    }
}
