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

        public async Task<IActionResult> Index(string? category)
        {
            // получить список категорий 
            var categoriesResponse = await
        _categoryService.GetCategoryListAsync();

            // если список не получен, вернуть код 404 
            if (!categoriesResponse.Success)
                return NotFound(categoriesResponse.ErrorMessage);

            // передать список категорий во ViewData        
            ViewData["categories"] = categoriesResponse.Data;

            // передать во ViewData имя текущей категории 
            var currentCategory = category == null
                ? "Все"
                : categoriesResponse.Data.FirstOrDefault(c =>
        c.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;


            var productResponse =
                        await
        _productService.GetProductListAsync(category);
            if (!productResponse.Success)
                ViewData["Error"] = productResponse.ErrorMessage;
            return View(productResponse.Data.Items);
        }
    }
}
