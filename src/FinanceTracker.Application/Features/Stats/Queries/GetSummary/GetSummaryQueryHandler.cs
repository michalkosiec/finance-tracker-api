using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Stats.Queries.GetSummary
{
    public class GetSummaryQueryHandler(IAppDbContext context)
        : IRequestHandler<GetSummaryQuery, StatsSummaryResponse>
    {
        public async Task<StatsSummaryResponse> Handle(
            GetSummaryQuery request,
            CancellationToken cancellationToken
        )
        {
            // change after adding proper currency handling

            var totalIncome = await context
                .Transactions.Where(t =>
                    t.UserId == request.UserId
                    && t.Date.Year == request.Year
                    && t.Date.Month == request.Month
                    && t.Type == TransactionType.Income
                )
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            var totalExpense = await context
                .Transactions.Where(t =>
                    t.UserId == request.UserId
                    && t.Date.Year == request.Year
                    && t.Date.Month == request.Month
                    && t.Type == TransactionType.Expense
                )
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            var balance = totalIncome - totalExpense;

            return new StatsSummaryResponse(
                $"{request.Year}-{request.Month:D2}",
                totalIncome,
                totalExpense,
                balance,
                request.Currency
            );
        }
    }
}
