using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using GR30323_Web.Domain.Services.CategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private readonly List<Car> _cars;
        private readonly List<Category> _categories;


        public MemoryProductService(ICategoryService categoryService)
        {
            var categoriesResponse = categoryService.GetCategoryListAsync().Result;
            if (categoriesResponse.Success)
            {
                _categories = categoriesResponse.Data;
            }
            else
            {
                _categories = new List<Category>(); // Или выбросить исключение, если категории обязательны
            }

            _cars = new List<Car>
    {
        new Car { Id = 1, Name = "Tesla Model S", Description = "Электрический седан", Price = 79999.99m, Image = "Images/tesla.jpg", CategoryId = _categories.ElementAtOrDefault(2)?.Id ?? 0 },
        new Car { Id = 2, Name = "Toyota Corolla", Description = "Седан C-класса", Price = 20999.99m, Image = "Images/corolla.jpg", CategoryId = _categories.ElementAtOrDefault(0)?.Id ?? 0 },
        new Car { Id = 3, Name = "Ford Explorer", Description = "Внедорожник", Price = 34999.99m, Image = "Images/explorer.jpg", CategoryId = _categories.ElementAtOrDefault(1)?.Id ?? 0 },
        new Car { Id = 4, Name = "VW Polo Sedan", Description = "Седан B-класса", Price = 34999.99m, Image = "Images/polo.jpg", CategoryId = _categories.ElementAtOrDefault(0)?.Id ?? 0 }
    };
        }

        public Task<ResponseData<ListModel<Car>>>
        GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
   
            // Создать объект результата 
            var result = new ResponseData<ListModel<Car>>();
            // Id категории для фильрации 
            int? categoryId = null;

            // если требуется фильтрация, то найти Id категории 
            // с заданным categoryNormalizedName 
            if (categoryNormalizedName != null)
                categoryId = _categories
                .Find(c =>
        c.NormalizedName.Equals(categoryNormalizedName))
                 ?.Id;

            // Выбрать объекты, отфильтрованные по Id категории, 
            // если этот Id имеется 
            var data = _cars
                .Where(d => categoryId == null ||
        d.CategoryId.Equals(categoryId))?
                .ToList();

            // поместить ранные в объект результата 
            result.Data = new ListModel<Car>() { Items = data };

            // Если список пустой 
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории"; 
            }
            // Вернуть результат 
            return Task.FromResult(result);
        }

        public Task<ResponseData<Car>> GetProductByIdAsync(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(new ResponseData<Car>
            {
                Data = car,
                Success = car != null,
                ErrorMessage = car == null ? "Автомобиль не найден" : null
            });
        }
    }
}
