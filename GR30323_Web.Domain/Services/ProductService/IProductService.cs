using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.ProductService
{
    public interface IProductService
    {
        // Получение списка автомобилей
        Task<ResponseData<ListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);

        // Получение автомобиля по ID
        Task<ResponseData<Car>> GetProductByIdAsync(int id);

        // Добавление нового автомобиля
        Task<ResponseData<Car>> AddCarAsync(Car car, IFormFile? image);

        // Обновление данных автомобиля
        Task<ResponseData<Car>> UpdateCarAsync(Car car, IFormFile? image);

        // Удаление автомобиля
        Task<ResponseData<Car>> DeleteCarAsync(int id);
    }
}
