using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Api.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController(ICategoryRepo repo, IMapper mapper, IValidationService validationService) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await repo.GetAllAsync(UserId!.Value);
            var categoriesRead = mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(categoriesRead);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await repo.GetByIdAsync(id, UserId!.Value);
            if (category == null)
            {
                return NotFound();
            } else
            {
                var categoryRead = mapper.Map<CategoryReadDto>(category);

                return Ok(categoryRead);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreate)
        {
            var category = mapper.Map<Category>(categoryCreate);
            category.UserId = UserId!.Value;

            await validationService.AllowCategory(category, UserId!.Value);

            await repo.CreateAsync(category);
            var categoryRead = mapper.Map<CategoryReadDto>(category);

            return CreatedAtAction(nameof(GetCategories), new { id = category }, categoryRead);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdate)
        {
            var category = await repo.GetByIdAsync(id, UserId!.Value);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                mapper.Map(categoryUpdate, category);
                category.UpdatedAt = DateTimeOffset.UtcNow;

                await validationService.AllowCategory(category, UserId!.Value);

                
                await repo.UpdateAsync(category, UserId!.Value);

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await repo.GetByIdAsync(id, UserId!.Value);
            if (category == null)
            {
                return NotFound();
            } else
            {
                await validationService.AllowCategoryDelete(category, UserId!.Value);
                await repo.DeleteAsync(id, UserId!.Value);

                return NoContent();
            }
        }
    }
}