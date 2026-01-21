using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.Game;

namespace TriviaGame.App.Services.interfaces
{
        public interface IGameApiService
    {
        Task<StartGameResponseDTO> StartGameAsync(StartGameRequestDTO request);
        Task<IEnumerable<GameQuestionDTO>> GetGameQuestionsAsync(int gameSessionId);
        Task<IEnumerable<AnswerDTO>> GetQuestionAnswersAsync(int questionId);
        Task SaveUserAnswerAsync(UserAnswerCreateDTO request);
        Task EndGameAsync(int gameSessionId);
        Task<IEnumerable<GameHistoryDTO>> GetUserGameHistoryAsync(int userId);
    }

}