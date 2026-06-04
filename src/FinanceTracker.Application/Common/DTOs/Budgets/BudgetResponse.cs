using FinanceTracker.Domain.ValueObjects;

namespace FinanceTracker.Application.Common.DTOs.Budgets
{
    public record BudgetResponse(
        Guid id,
        Guid userId,
        Guid categoryId,
        Money limitAmount,
        DateTime month,
        DateTime createdAt,
        DateTime updatedAt
        )
    { }
}