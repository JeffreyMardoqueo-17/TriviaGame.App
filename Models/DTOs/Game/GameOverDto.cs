using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
    public class GameOverDto
    {
        public int TotalScore { get; set; }
        public List<RankingDto> Ranking { get; set; } = new();
    }
}