using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.ProductService
{
    public interface IProductService
    {
        /// <summary>
        /// Получение списка всех автомобилей с фильтрацией по категориям.
        /// </summary>
        /// <param name="categoryNormalizedName">Имя категории для фильтрации (или null для всех категорий).</param>
        /// <param name="pageNo">Номер страницы.</param>
        /// <returns>Список автомобилей.</returns>
        Task<ResponseData<ListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);

        /// <summary>
        /// Получение автомобиля по ID.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        /// <returns>Найденный автомобиль или null.</returns>
        Task<ResponseData<Car>> GetProductByIdAsync(int id);
    }
}
