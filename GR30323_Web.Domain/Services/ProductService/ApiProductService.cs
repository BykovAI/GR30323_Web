using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using GR30323_Web.Domain.Services.ProductService;

namespace GR30323_Web.UI.Services
{
    public class ApiProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ApiProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<ListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var url = $"?categoryNormalizedName={categoryNormalizedName}&pageNo={pageNo}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new ResponseData<ListModel<Car>> { Success = false, ErrorMessage = "Ошибка при получении данных" };

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseData<ListModel<Car>>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result!;
        }

        public async Task<ResponseData<Car>> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");

            if (!response.IsSuccessStatusCode)
                return new ResponseData<Car> { Success = false, ErrorMessage = "Автомобиль не найден" };

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseData<Car>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result!;
        }
    }
}
