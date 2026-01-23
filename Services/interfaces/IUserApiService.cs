using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.User;

namespace TriviaGame.App.Services.interfaces
{
    public interface IUserApiService
    {
        Task<UserResponseDto?> LoginAsync(LoginUserRequestDto loginDto);
        Task<UserResponseDto?> RegisterAsync(RegisterUserRequestDto registerDto);
    }
}