using MediatR;

namespace FinanceTracker.Application.Features.Stats.Queries.GetMonthlyStats
{
    public record MonthlyStatResponse(string Month, decimal TotalIncome, decimal TotalExpense);

    public record MonthlyStatsResponse(
        IReadOnlyCollection<MonthlyStatResponse> MonthlyStats,
        string Currency
    );

    public record GetMonthlyStatsQuery(int Year, string Currency, Guid UserId)
        : IRequest<MonthlyStatsResponse> { }
}
