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

        // ------------------------
        // Vista de preguntas del juego
        // ------------------------
        [HttpGet("Play")]
        public async Task<IActionResult> Play(int gameSessionId)
        {
            if (gameSessionId <= 0)
                return RedirectToAction("Index", "Home");

            var questions = await _gameApiService.GetGameQuestionsAsync(gameSessionId);
            return View(questions);
        }

        // ------------------------
        // Obtiene las respuestas de una pregunta (AJAX)
        // ------------------------
        [HttpGet]
        public async Task<IActionResult> GetAnswers(int questionId)
        {
            try
            {
                var answers = await _gameApiService.GetQuestionAnswersAsync(questionId);
                return Json(answers);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error cargando respuestas");
                return StatusCode(500, ex.Message);
            }
        }

        // ------------------------
        // Guarda la respuesta del usuario (AJAX)
        // ------------------------
        [HttpPost]
        public async Task<IActionResult> SaveAnswer(int questionId, int answerId, int timeSpentSeconds)
        {
            try
            {
                var gameSessionId = HttpContext.Session.GetInt32("GameSessionId");
                if (!gameSessionId.HasValue)
                    return BadRequest("No hay sesión de juego activa.");

                var request = new UserAnswerCreateDTO
                {
                    GameSessionId = gameSessionId.Value,
                    QuestionId = questionId,
                    AnswerId = answerId,
                    TimeSpentSeconds = timeSpentSeconds
                };

                await _gameApiService.SaveUserAnswerAsync(request);

                return Ok(new { message = "Respuesta registrada correctamente" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error guardando respuesta");
                return StatusCode(500, ex.Message);
            }
        }

        // ------------------------
        // Finaliza la sesión del juego
        // ------------------------
        [HttpPost]
        public async Task<IActionResult> EndGame()
        {
            try
            {
                var gameSessionId = HttpContext.Session.GetInt32("GameSessionId");
                if (!gameSessionId.HasValue)
                    return BadRequest("No hay sesión de juego activa.");

                await _gameApiService.EndGameAsync(gameSessionId.Value);

                // Limpiamos la sesión
                HttpContext.Session.Remove("GameSessionId");

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error finalizando el juego");
                TempData["Error"] = ex.Message;
                return RedirectToAction("Play");
            }
        }
    }
}
