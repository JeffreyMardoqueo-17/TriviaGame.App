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

            // Guardar en Session
            HttpContext.Session.SetString("JWT", user.Token);
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserEmail", user.Gmail);

            await HttpContext.Session.CommitAsync();

            // TempData para JS
            TempData["JWT"] = user.Token;
            TempData["UserId"] = user.Id.ToString();

            return RedirectToAction("Index", "Category");
        }


        // Mostrar formulario de registro
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserRequestDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _userApiService.RegisterAsync(model);

            if (response == null)
            {
                ModelState.AddModelError("", "Error al registrar el usuario");
                return View(model);
            }

            // Si el usuario ya existe
            if (!response.Success || !string.IsNullOrEmpty(response.Message))
            {
                ModelState.AddModelError("", response.Message ?? "Error al registrar el usuario");
                return View(model);
            }

            // Registro exitoso
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
