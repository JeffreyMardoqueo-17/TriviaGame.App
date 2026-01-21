using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.Game;
using TriviaGame.App.Services.interfaces;
using Microsoft.AspNetCore.Http;

namespace TriviaGame.App.Services.Service
{
    public class GameApiService : IGameApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GameApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            // si ya hay  JWT guardado en session, agregamos al header
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<StartGameResponseDTO> StartGameAsync(StartGameRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Game/start", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StartGameResponseDTO>();
        }
    }
}
