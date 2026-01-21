using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs.User
{
    public class RegisterUserRequestDto
    {
        public string Gmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
       
    }
}