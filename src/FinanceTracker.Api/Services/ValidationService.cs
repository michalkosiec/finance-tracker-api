using FinanceTracker.Api.Models;
using FinanceTracker.Api.Repositories.Interfaces;
using FinanceTracker.Api.Services.Interfaces;

namespace FinanceTracker.Api.Services
{
    public class ValidationService(IBudgetRepo budgetRepo, ITransactionRepo transactionRepo, ICategoryRepo categoryRepo) : IValidationService
    {
        public async Task AllowBudget(Budget budget, Guid userId)
        {
            var budgetId = budget.Id;
            var categoryId = budget.CategoryId;
            var limitAmount = budget.LimitAmount;
            var month = budget.Month.Month;
            var year = budget.Month.Year;

            if(await budgetRepo.AnyAsync(b => 
                b.Id != budgetId &&
                b.CategoryId == categoryId &&
                b.Month.Month == month &&
                b.Month.Year == year,
                userId))
            {
                throw new BadHttpRequestException("Budget for the given month already exists.");
            }

            decimal totalAmount = await transactionRepo.GetTotalSpendingAsync(userId, categoryId, budget.Month, null);
           
            if(totalAmount > limitAmount)
            {
                throw new BadHttpRequestException($"Cannot set the budget limit ({limitAmount}) below the already spent amount ({totalAmount}).");
            }
        }

        public async Task AllowCategory(Category category, Guid userId)
        {
            var nameExists = await categoryRepo.AnyAsync(c => c.Name == category.Name, userId);

            if(nameExists)
            {
                throw new BadHttpRequestException("Category with the given name already exists.");
            }
        }

        public async Task AllowCategoryDelete(Category category, Guid userId)
        {
            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this category.");
            }

            var isUsedInBudgets = await budgetRepo.AnyAsync(b => b.CategoryId == category.Id, userId);

            if (isUsedInBudgets)
            {
                throw new BadHttpRequestException("Cannot delete this category because it is currently linked to one or more budgets.");
            }

        }

        public async Task AllowTransaction(Transaction transaction, Guid userId)
        {
           var categoryId = transaction.CategoryId;
           var amount = transaction.Amount;

           if (!await categoryRepo.AnyAsync(c => c.Id == categoryId, userId))
            {
                throw new KeyNotFoundException("Given category does not exist.");
            }

           var budget = await budgetRepo.GetByCategoryAsync(categoryId);

           if (budget is null) return;

           var month = budget.Month;

           decimal totalAmount = amount + await transactionRepo.GetTotalSpendingAsync(userId, categoryId, month, transaction.Id);
           
           if (transaction.Type == TransactionType.Expense && totalAmount > budget.LimitAmount)
            {
                throw new BadHttpRequestException($"Transaction denied. Adding this expense ({amount}) would exceed the category budget limit of {budget.LimitAmount}. Total spending would be {totalAmount}.");
            }
        }
    }
}