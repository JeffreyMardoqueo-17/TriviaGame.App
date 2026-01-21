using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.User;
using TriviaGame.App.Services.interfaces;

namespace TriviaGame.App.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Login de usuario
        public async Task<UserResponseDto?> LoginAsync(LoginUserRequestDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginDto);
            if (!response.IsSuccessStatusCode)
            {
                // Retornar null si falla el login
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserResponseDto>();
        }

        // Registro de usuario
        public async Task<UserResponseDto?> RegisterAsync(RegisterUserRequestDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", registerDto);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<UserResponseDto>();
        }
    }
}
