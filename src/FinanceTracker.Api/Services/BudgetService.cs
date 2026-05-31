using AutoMapper;
using FinanceTracker.Api.Dtos.Budgets;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Services
{
    public class BudgetService(IBudgetRepo budgetRepo, IMapper mapper, IValidationService validationService) : IBudgetService
    {
        public async Task<BudgetReadDto> CreateBudgetAsync(Guid userId, BudgetCreateDto budgetCreate)
        {
            var budget = mapper.Map<Budget>(budgetCreate);
            budget.UserId = userId;

            await validationService.AllowBudget(budget, userId);

            await budgetRepo.CreateAsync(budget);

            return mapper.Map<BudgetReadDto>(budget);
        }

        public async Task<bool> DeleteBudgetAsync(Guid userId, Guid budgetId)
        {
            var budget = await budgetRepo.GetByIdAsync(budgetId, userId);
            if (budget == null)
                return false;

            await budgetRepo.DeleteAsync(budgetId, userId);
            
            return true;
        }

        public async Task<IEnumerable<BudgetReadDto>> GetAllBudgetsAsync(Guid userId)
        {
            var budgets = await budgetRepo.GetAllAsync(userId);

            return mapper.Map<IEnumerable<BudgetReadDto>>(budgets);
        }

        public async Task<BudgetReadDto?> GetBudgetByIdAsync(Guid userId, Guid budgetId)
        {
            var budget = await budgetRepo.GetByIdAsync(budgetId, userId);

            return mapper.Map<BudgetReadDto>(budget);
        }

        public async Task<bool> UpdateBudgetAsync(Guid userId, Guid budgetId, BudgetUpdateDto budgetUpdate)
        {
            var budget = await budgetRepo.GetByIdAsync(budgetId, userId);
            if (budget == null)
             return false;
            
            mapper.Map(budgetUpdate, budget);
            budget.UserId = userId;

            budget.UpdatedAt = DateTimeOffset.UtcNow;

            await validationService.AllowBudget(budget, userId);
                
            await budgetRepo.UpdateAsync(budget, userId);

            return true;
        }
    }
}