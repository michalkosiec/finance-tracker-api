using AutoMapper;
using FinanceTracker.Api.Dtos.Categories;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Services
{
    public class CategoryService(ICategoryRepo categoryRepo, IMapper mapper, IValidationService validationService) : ICategoryService
    {
        public async Task<CategoryReadDto> CreateCategoryAsync(Guid userId, CategoryCreateDto categoryCreate)
        {
            var category = mapper.Map<Category>(categoryCreate);
            category.UserId = userId;

            await validationService.AllowCategory(category, userId);

            await categoryRepo.CreateAsync(category);

            return mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(Guid userId, Guid categoryId)
        {
            var category = await categoryRepo.GetByIdAsync(categoryId, userId);
            if (category == null)
                return false;

            await validationService.AllowCategoryDelete(category, userId);
            await categoryRepo.DeleteAsync(categoryId, userId);

            return true;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync(Guid userId)
        {
            var categories = await categoryRepo.GetAllAsync(userId);

            return mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetCategoryByIdAsync(Guid userId, Guid categoryId)
        {
            var category = await categoryRepo.GetByIdAsync(categoryId, userId);

            return mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(Guid userId, Guid categoryId, CategoryUpdateDto categoryUpdate)
        {
            var category = await categoryRepo.GetByIdAsync(categoryId, userId);
            if (category == null)
                return false;

            mapper.Map(categoryUpdate, category);
            category.UpdatedAt = DateTimeOffset.UtcNow;

            await validationService.AllowCategory(category, userId);

            await categoryRepo.UpdateAsync(category, userId);

            return true;
        }
    }
}