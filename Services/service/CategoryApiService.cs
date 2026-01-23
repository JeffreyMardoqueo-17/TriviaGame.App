using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TriviaGame.App.Models.DTOs.Category;
using TriviaGame.App.Services.interfaces;

namespace TriviaGame.App.Services.service
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync(string jwtToken)
        {
            // Configura el token en Authorization header
            if (!string.IsNullOrEmpty(jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var response = await _httpClient.GetAsync("api/Category");

            if (!response.IsSuccessStatusCode)
                return new List<CategoryResponseDTO>();

            var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryResponseDTO>>();
            return categories ?? new List<CategoryResponseDTO>();
        }
    }
}
