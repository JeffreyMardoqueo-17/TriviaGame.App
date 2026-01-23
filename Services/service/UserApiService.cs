using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.User;
using TriviaGame.App.Services.interfaces;
using System.Text.Json;

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
                return null;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await response.Content.ReadFromJsonAsync<UserResponseDto>(options);
        }

        public async Task<UserResponseDto?> RegisterAsync(RegisterUserRequestDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", registerDto);

            try
            {
                var userResponse = await response.Content.ReadFromJsonAsync<UserResponseDto>();
                return userResponse;
            }
            catch
            {
                // Retorna un objeto con mensaje genérico si falla
                return new UserResponseDto
                {
                    Success = false,
                    Message = "Error al registrar usuario"
                };
            }
        }
    }
}
