using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryService categoryService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoriesRead = await categoryService.GetAllCategoriesAsync(UserId!.Value);

            return Ok(categoriesRead);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var categoryRead = await categoryService.GetCategoryByIdAsync(UserId!.Value, id);
            
            return categoryRead == null ? NotFound() : Ok(categoryRead);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreate)
        {
            var categoryRead = await categoryService.CreateCategoryAsync(UserId!.Value, categoryCreate);

            return CreatedAtAction(nameof(GetCategories), new { id = categoryRead.Id }, categoryRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdate)
        {
            var result = await categoryService.UpdateCategoryAsync(UserId!.Value, id, categoryUpdate);

            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await categoryService.DeleteCategoryAsync(UserId!.Value, id);

            return result ? NoContent() : NotFound();
        }
    }
}