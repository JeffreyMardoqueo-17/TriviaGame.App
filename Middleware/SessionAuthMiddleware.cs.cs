using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
            var path = context.Request.Path.Value?.ToLower() ?? "";

            // Rutas públicas que no requieren login
            if (path.StartsWith("/account/login") ||
                path.StartsWith("/account/register") ||
                path.StartsWith("/css") ||
                path.StartsWith("/js") ||
                path.StartsWith("/lib"))
            {
                await _next(context);
                return;
            }

            //------ Cargar sesiinn (obligatorio)
            await context.Session.LoadAsync();

            var jwt = context.Session.GetString("JWT");

            if (string.IsNullOrEmpty(jwt))
            {
                //------ Redirige al login si no hay JWT
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }
    }
}