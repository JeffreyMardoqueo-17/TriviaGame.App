using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
    public class GameHistoryDTO
    {
        public int GameSessionId { get; set; }
        public string Category { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public int TimeSpentSeconds { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}