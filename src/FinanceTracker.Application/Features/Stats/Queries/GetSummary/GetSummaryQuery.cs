using MediatR;

namespace FinanceTracker.Application.Features.Stats.Queries.GetSummary
{
    public record StatsSummaryResponse(
        string Month,
        decimal TotalIncome,
        decimal TotalExpense,
        decimal Balance,
        string Currency
    ) { }

    public record GetSummaryQuery(int Year, int Month, string Currency, Guid UserId)
        : IRequest<StatsSummaryResponse> { }
}
