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
    }

}