using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using GR30323_Web.Domain.Services.CategoryService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private readonly List<Car> _cars;
        private readonly List<Category> _categories;
        private readonly IConfiguration _config;

        public MemoryProductService(IConfiguration config, ICategoryService categoryService)
        {
            _config = config;

            var categoriesResponse = categoryService.GetCategoryListAsync().Result;
            _categories = categoriesResponse.Success ? categoriesResponse.Data : new List<Category>();

            _cars = new List<Car>
            {
                new Car { Id = 1, Name = "Tesla Model S", Description = "Электрический седан", Price = 79999.99m, Image = "Images/tesla.jpg", CategoryId = _categories.ElementAtOrDefault(2)?.Id ?? 0 },
                new Car { Id = 2, Name = "Toyota Corolla", Description = "Седан C-класса", Price = 20999.99m, Image = "Images/corolla.jpg", CategoryId = _categories.ElementAtOrDefault(0)?.Id ?? 0 },
                new Car { Id = 3, Name = "Ford Explorer", Description = "Внедорожник", Price = 34999.99m, Image = "Images/explorer.jpg", CategoryId = _categories.ElementAtOrDefault(1)?.Id ?? 0 },
                new Car { Id = 4, Name = "VW Polo Sedan", Description = "Седан B-класса", Price = 34999.99m, Image = "Images/polo.jpg", CategoryId = _categories.ElementAtOrDefault(0)?.Id ?? 0 }
            };
        }

        public Task<ResponseData<ListModel<Car>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var result = new ResponseData<ListModel<Car>>();
            int? categoryId = null;

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                categoryId = _categories.FirstOrDefault(c => c.NormalizedName == categoryNormalizedName)?.Id;
            }

            var filteredCars = _cars
                .Where(car => categoryId == null || car.CategoryId == categoryId)
                .ToList();

            int pageSize = _config.GetValue<int>("ItemsPerPage");
            int totalPages = (int)Math.Ceiling(filteredCars.Count / (double)pageSize);

            var paginatedCars = filteredCars
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            result.Data = new ListModel<Car>
            {
                Items = paginatedCars,
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            if (!paginatedCars.Any())
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

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
