using GR30323_Web.Domain.Entities;
using GR30323_Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Services.CategoryService
{
    public interface ICategoryService
    {
        /// <summary> 
        /// Получение списка всех категорий 
        /// </summary> 
        /// <returns></returns> 
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
