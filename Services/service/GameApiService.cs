using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using TriviaGame.App.Services.interfaces;
using TriviaGame.App.Models.DTOs.Game;
using System.Net.Http.Json;


namespace TriviaGame.App.Services.service
{
    public class GameApiService : IGameApiService
    {
        private readonly HttpClient _httpClient;

        public GameApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameSessionDto?> StartGameAsync(int userId, int categoryId)
        {
            var response = await _httpClient
                .PostAsJsonAsync(
                    $"api/Game/start?userId={userId}&categoryId={categoryId}",
                    new { }
                );

            return await response.Content.ReadFromJsonAsync<GameSessionDto>();
        }

        public async Task<QuestionDto?> GetNextQuestionAsync(int gameSessionId)
        {
            return await _httpClient
                .GetFromJsonAsync<QuestionDto>(
                    $"api/Game/{gameSessionId}/next-question"
                );
        }

        public async Task<AnswerResultDto?> SubmitAnswerAsync(SubmitAnswerDto dto)
        {
            var response = await _httpClient
                .PostAsJsonAsync("api/Game/submit-answer", dto);

            return await response.Content.ReadFromJsonAsync<AnswerResultDto>();
        }

        public async Task<GameOverDto?> EndGameAsync(int gameSessionId)
        {
            var response = await _httpClient
                .PostAsync($"api/Game/{gameSessionId}/end", null);

            return await response.Content.ReadFromJsonAsync<GameOverDto>();
        }

        public async Task<List<RankingDto>> GetRankingAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<RankingDto>>("api/Game/ranking")
                ?? new();
        }
    
    }
}