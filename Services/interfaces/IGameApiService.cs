using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.Game;

namespace TriviaGame.App.Services.interfaces
{
    public interface IGameApiService
    {
        Task<GameSessionDto?> StartGameAsync(int userId, int categoryId);
        Task<QuestionDto?> GetNextQuestionAsync(int gameSessionId);
        Task<AnswerResultDto?> SubmitAnswerAsync(SubmitAnswerDto dto);
        Task<GameOverDto?> EndGameAsync(int gameSessionId);
        Task<List<CategoryRankingDto>> GetCategoryRankingAsync(int categoryId, int top = 10);
    }
}