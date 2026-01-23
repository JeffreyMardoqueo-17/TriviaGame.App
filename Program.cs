using TriviaGame.App.Middlewares;
using TriviaGame.App.Services;
using TriviaGame.App.Services.interfaces;
using TriviaGame.App.Services.service;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// cnfiguracion HttpClient para los servicios API
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7235/";
//agregos los servico y controllers
builder.Services.AddHttpClient<IUserApiService, UserApiService>(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<IGameApiService, GameApiService>(c => c.BaseAddress = new Uri(apiBaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// SESSION primero
app.UseSession();

// Luego el  middleware de autenticación
// app.UseMiddleware<SessionAuthMiddleware>();

// Auth/Authorization 
app.UseAuthentication();
app.UseAuthorization();

// Rutas de los controladores 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"); //ESTA POR DEFECTO PORQUE EL SUAURIO SIEMPRE QUIERE INICIAR SESION (LO DE LA HISTORIA DE USAIOR DE LO QUE REQUERIA en la prueba )

app.Run();
