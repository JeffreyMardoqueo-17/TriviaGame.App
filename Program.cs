using TriviaGame.App.Services;
using TriviaGame.App.Services.interfaces;
using TriviaGame.App.Services.service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Necesario si tus servicios usan HttpContext (para JWT o sesión)
builder.Services.AddHttpContextAccessor();


// HttpClient para CategoryApiService
builder.Services.AddHttpClient<ICategoryApiService, CategoryApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7235/"); // tu backend
});

// HttpClient para UserApiService
builder.Services.AddHttpClient<IUserApiService, UserApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7235/");
});
// httpclient para GameApiService
builder.Services.AddHttpClient<IGameApiService, GameApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7235/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    // ⚠️ Solo para desarrollo: ignora errores de certificado
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
