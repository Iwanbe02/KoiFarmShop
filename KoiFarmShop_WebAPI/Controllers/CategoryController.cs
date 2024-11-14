using BusinessObjects.Models;
using DataAccessObjects.DTOs.CategoryDTO;
using DataAccessObjects.DTOs.KoiFishDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await _categoryService.GetAllCategories();
            return Ok(category);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            var category = await _categoryService.CreateCategory(createCategoryDTO);
            return Ok(category);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory(int Id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _categoryService.UpdateCategory(Id, updateCategoryDTO);
            return Ok(category);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreCategory(int Id)
        {
            await _categoryService.RestoreCategory(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var category = await _categoryService.DeleteCategory(Id);
            return Ok();
        }
    }
}
