using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using GR30323_Web.Domain.Services.CategoryService;
using GR30323_Web.Domain.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace GR30323_Web.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("Catalog")]
        [Route("Catalog/{category}")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            // Получаем список категорий
            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            if (!categoriesResponse.Success)
            {
                return NotFound(categoriesResponse.ErrorMessage);
            }

            // Передаем категории в ViewData
            ViewData["categories"] = categoriesResponse.Data;

            // Получаем список продуктов с пагинацией
            var productsResponse = await _productService.GetProductListAsync(category, pageNo);
            if (!productsResponse.Success)
            {
                ViewData["Error"] = productsResponse.ErrorMessage;
            }

            // Создаем модель для передачи в представление, которая включает в себя данные о текущей странице и количестве страниц
            var listModel = new ListModel<Car>
            {
                Items = productsResponse.Data.Items,
                CurrentPage = pageNo,
                TotalPages = productsResponse.Data.TotalPages
            };

            // Возвращаем модель ListModel<Car> в представление
            return View(listModel);
        }

    }
}
