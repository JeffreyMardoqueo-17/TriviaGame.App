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

            // Si tienes JWT guardado en session, agregamos al header
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

        public async Task<IEnumerable<GameQuestionDTO>> GetGameQuestionsAsync(int gameSessionId)
        {
            var response = await _httpClient.GetAsync($"api/Game/{gameSessionId}/questions");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<GameQuestionDTO>>();
        }

        public async Task<IEnumerable<AnswerDTO>> GetQuestionAnswersAsync(int questionId)
        {
            var response = await _httpClient.GetAsync($"api/Game/questions/{questionId}/answers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<AnswerDTO>>();
        }

        public async Task SaveUserAnswerAsync(UserAnswerCreateDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Game/answer", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task EndGameAsync(int gameSessionId)
        {
            var response = await _httpClient.PostAsync($"api/Game/{gameSessionId}/end", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<GameHistoryDTO>> GetUserGameHistoryAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/Game/history/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<GameHistoryDTO>>();
        }
    }
}
