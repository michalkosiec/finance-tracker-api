using MediatR;

namespace FinanceTracker.Application.Features.Stats.Queries.GetExpensesByCategory
{
    public record CategoryStatResponse(
        string CategoryName,
        decimal TotalExpense,
        int NumberOfTransactions
    );

    public record ExpensesByCategoryResponse(
        IReadOnlyCollection<CategoryStatResponse> CategoryStats,
        string Currency
    );

    public record GetExpensesByCategoryQuery(int Year, int Month, string Currency, Guid UserId)
        : IRequest<ExpensesByCategoryResponse>;
}
