using BusinessObjects.Models;
using DataAccessObjects.DTOs.CategoryDTO;
using DataAccessObjects.DTOs.OrderDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(CreateCategoryDTO createCategory)
        {
            var category = new Category
            {
                Category1 = createCategory.Category1,
            };
            await _categoryRepository.AddAsync(category);
            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with ID{id} is not found");
            }
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> RestoreCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with ID{id} is not found");
            }
            if (category.IsDeleted == true)
            {
                category.IsDeleted = false;
                await _categoryRepository.UpdateAsync(category);
            }
            return category;
        }

        public async Task<Category> UpdateCategory(int id, UpdateCategoryDTO updateCategory)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with ID{id} is not found");
            }
            category.Category1 = updateCategory.Category1;
            await _categoryRepository.UpdateAsync(category);
            return category;
        }
    }
}
