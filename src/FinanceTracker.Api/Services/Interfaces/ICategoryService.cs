using FinanceTracker.Api.Dtos.Categories;

namespace FinanceTracker.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync(Guid userId);
        public Task<CategoryReadDto?> GetCategoryByIdAsync(Guid userId, Guid categoryId);
        public Task<CategoryReadDto> CreateCategoryAsync(Guid userId, CategoryCreateDto categoryCreate);
        public Task<bool> UpdateCategoryAsync(Guid userId, Guid categoryId, CategoryUpdateDto categoryUpdate);
        public Task<bool> DeleteCategoryAsync(Guid userId, Guid categoryId);
    }
}