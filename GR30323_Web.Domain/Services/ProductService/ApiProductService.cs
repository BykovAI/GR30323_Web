using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.ProductService
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
            var response = await _httpClient.GetFromJsonAsync<ResponseData<ListModel<Car>>>($"api/cars?category={categoryNormalizedName}&page={pageNo}");
            return response ?? new ResponseData<ListModel<Car>> { Success = false, ErrorMessage = "Error fetching cars." };
        }

        public async Task<ResponseData<Car>> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseData<Car>>($"api/cars/{id}");
            return response ?? new ResponseData<Car> { Success = false, ErrorMessage = "Car not found." };
        }

        public async Task<ResponseData<Car>> AddCarAsync(Car car, IFormFile? image)
        {
            var response = await _httpClient.PostAsJsonAsync("api/cars", car);
            if (response.IsSuccessStatusCode)
            {
                var addedCar = await response.Content.ReadFromJsonAsync<Car>();
                return new ResponseData<Car> { Success = true, Data = addedCar };
            }
            return new ResponseData<Car> { Success = false, ErrorMessage = "Error adding car." };
        }

        public async Task<ResponseData<Car>> UpdateCarAsync(Car car, IFormFile? image)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/cars/{car.Id}", car);
            if (response.IsSuccessStatusCode)
            {
                var updatedCar = await response.Content.ReadFromJsonAsync<Car>();
                return new ResponseData<Car> { Success = true, Data = updatedCar };
            }
            return new ResponseData<Car> { Success = false, ErrorMessage = "Error updating car." };
        }

        public async Task<ResponseData<Car>> DeleteCarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                return new ResponseData<Car> { Success = true };
            }
            return new ResponseData<Car> { Success = false, ErrorMessage = "Error deleting car." };
        }
    }
}
