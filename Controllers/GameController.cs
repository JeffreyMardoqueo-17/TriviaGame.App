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

            // ------------------------
            // Vista principal del juego (categorías)
            // ------------------------
            public async Task<IActionResult> Index()
            {
                // Aquí idealmente recibes las categorías desde CategoryApiService
                // Por simplicidad asumimos que el frontend ya tiene las categorías cargadas
                return View();
            }

            // ------------------------
            // Inicia la sesión del juego
            // ------------------------
            [HttpPost]
            public async Task<IActionResult> StartGame(int userId, int categoryId)
            {
                try
                {
                    var request = new StartGameRequestDTO
                    {
                        UserId = userId,
                        CategoryId = categoryId
                    };

                    var result = await _gameApiService.StartGameAsync(request);

                    // Guardamos el GameSessionId en Session para usarlo en el juego
                    HttpContext.Session.SetInt32("GameSessionId", result.GameSessionId);

                    // Redirigir a la vista de preguntas
                    return RedirectToAction("Play", new { gameSessionId = result.GameSessionId });
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error iniciando el juego");
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }
        }
    }
