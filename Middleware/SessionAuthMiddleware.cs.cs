using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace TriviaGame.App.Middlewares
{
    public class SessionAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";
            Console.WriteLine($"[Middleware] Request Path: {path}");

            if (path == "/" ||
     path.StartsWith("/account/login", StringComparison.OrdinalIgnoreCase) ||
     path.StartsWith("/account/register", StringComparison.OrdinalIgnoreCase) ||
     path.StartsWith("/css") ||
     path.StartsWith("/js") ||
     path.StartsWith("/lib"))
            {
                await _next(context);
                return;
            }

            // Revisar JWT en Session
            var jwt = context.Session.GetString("JWT");
            Console.WriteLine($"[Middleware] JWT en Session: {(jwt ?? "null")}");

            if (string.IsNullOrEmpty(jwt))
            {
                Console.WriteLine("[Middleware] No hay sesión, redirigiendo al login...");
                context.Response.Redirect("/Account/Login");
                return;
            }

            Console.WriteLine("[Middleware] Sesión encontrada, continuando al siguiente middleware/controller...");
            await _next(context);
        }
    }
}
