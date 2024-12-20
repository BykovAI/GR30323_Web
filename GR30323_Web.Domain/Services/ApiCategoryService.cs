using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using GR30323_Web.Domain.Services.CategoryService;

namespace GR30323_Web.UI.Services
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public ApiCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var response = await _httpClient.GetAsync("");

            if (!response.IsSuccessStatusCode)
                return new ResponseData<List<Category>> { Success = false, ErrorMessage = "Ошибка при получении категорий" };

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseData<List<Category>>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result!;
        }
    }
}
