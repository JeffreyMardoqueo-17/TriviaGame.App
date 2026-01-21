using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
    public class GameQuestionDTO
    {
         public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int Points { get; set; }
        public int TimeLimitSeconds { get; set; }
    }
}