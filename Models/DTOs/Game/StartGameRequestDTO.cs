using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.Game
{
    public class StartGameRequestDTO
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}