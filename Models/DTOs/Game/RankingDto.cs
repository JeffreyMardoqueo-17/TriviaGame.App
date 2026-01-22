using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
     public class RankingDto
    {
        public int UserId { get; set; }
        public string Gmail { get; set; } = string.Empty;
        public int TotalPoints { get; set; }
    }
}