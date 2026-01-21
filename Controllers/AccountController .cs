using Microsoft.AspNetCore.Mvc;
using TriviaGame.App.Models.DTOs.User;
using TriviaGame.App.Services;
using TriviaGame.App.Services.interfaces;

namespace TriviaGame.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiService _userApiService;

        public AccountController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        // Mostrar formulario de login
        [HttpGet]
        public IActionResult Login() => View();

        // Procesar login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserRequestDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userApiService.LoginAsync(model);

            if (user == null || string.IsNullOrEmpty(user.Token))
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View(model);
            }

            // Guardar token y correo en session
            HttpContext.Session.SetString("JWT", user.Token);
            HttpContext.Session.SetString("UserEmail", user.Gmail);
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return RedirectToAction("Index", "Category");
        }


        // Mostrar formulario de registro
        [HttpGet]
        public IActionResult Register() => View();

        // Procesar registro
        [HttpPost]
        [ValidateAntiForgeryToken] //proetccioin contra ataques CSRF
        public async Task<IActionResult> Register(RegisterUserRequestDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userApiService.RegisterAsync(model);

            if (user == null || !user.Success)
            {
                ModelState.AddModelError("", user?.Message ?? "Error al registrar");
                return View(model);
            }

            return RedirectToAction("Login");
        }
    }
}
