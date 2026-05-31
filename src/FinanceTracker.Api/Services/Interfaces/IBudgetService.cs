using FinanceTracker.Api.Dtos.Budgets;

namespace FinanceTracker.Api.Services
{
    public interface IBudgetService
    {
        public Task<IEnumerable<BudgetReadDto>> GetAllBudgetsAsync(Guid userId);
        public Task<BudgetReadDto?> GetBudgetByIdAsync(Guid userId, Guid budgetId);
        public Task<BudgetReadDto> CreateBudgetAsync(Guid userId, BudgetCreateDto budgetCreate);
        public Task<bool> UpdateBudgetAsync(Guid userId, Guid budgetId, BudgetUpdateDto budgetUpdate);
        public Task<bool> DeleteBudgetAsync(Guid userId, Guid budgetId);
    }
}