using BusinessObjects.Models;
using DataAccessObjects.DTOs.CategoryDTO;
using DataAccessObjects.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICategoryService 
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(CreateCategoryDTO createCategory);
        Task<Category> UpdateCategory(int id, UpdateCategoryDTO updateCategory);
        Task<Category> DeleteCategory(int id);
        Task<Category> RestoreCategory(int id);
    }
}
