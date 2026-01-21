using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; } = string.Empty;

    }
}