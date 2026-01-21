using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaGame.App.Models.DTOs.Category;

namespace TriviaGame.App.Services.interfaces
{
    public interface ICategoryApiService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync(string jwtToken);
    }
}