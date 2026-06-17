namespace FinanceTracker.Application.Common.DTOs.Budgets
{
    public record BudgetResponse(
        Guid Id,
        Guid UserId,
        Guid CategoryId,
        decimal LimitAmount,
        string Currency,
        DateTime Month,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt
    ) { }
}
