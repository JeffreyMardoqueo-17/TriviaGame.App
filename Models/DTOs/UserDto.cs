using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaGame.App.Models.DTOs
{
    public class UserDto
    {
        // LoginUserRequestDto
        public class LoginUserRequestDto
        {
            public string Gmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        // RegisterUserRequestDto
        public class RegisterUserRequestDto
        {
            public string Gmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        // UserResponseDto
        public class UserResponseDto
        {
            public int Id { get; set; }
            public string Gmail { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public string Token { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public bool Success { get; set; }
        }
    }
}