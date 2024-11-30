using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new()
        {
            new Category { Id = 1, Name = "Седаны", NormalizedName = "sedans" },
            new Category { Id = 2, Name = "Внедорожники", NormalizedName = "SUVs" },
            new Category { Id = 3, Name = "Электромобили", NormalizedName = "electric-cars" }
        };

        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            return Task.FromResult(new ResponseData<List<Category>> { Data = _categories });
        }
    }
}
