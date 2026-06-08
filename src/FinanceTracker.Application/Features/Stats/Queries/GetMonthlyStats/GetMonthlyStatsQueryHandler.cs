using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Stats.Queries.GetMonthlyStats
{
    public class GetMonthlyStatsQueryHandler(IAppDbContext context)
        : IRequestHandler<GetMonthlyStatsQuery, MonthlyStatsResponse>
    {
        public async Task<MonthlyStatsResponse> Handle(
            GetMonthlyStatsQuery request,
            CancellationToken cancellationToken
        )
        {
            // change after implementing currency handling
            var rawStats = await context
                .Transactions.Where(t => t.UserId == request.UserId && t.Date.Year == request.Year)
                .GroupBy(t => new { t.Date.Month, t.Type })
                .Select(g => new
                {
                    g.Key.Month,
                    g.Key.Type,
                    Total = g.Sum(t => t.Amount.Amount),
                })
                .ToListAsync(cancellationToken);

            var monthlyStats = new List<MonthlyStatResponse>(12);

            for (int i = 1; i <= 12; i++)
            {
                var income =
                    rawStats
                        .FirstOrDefault(x => x.Month == i && x.Type == TransactionType.Income)
                        ?.Total
                    ?? 0m;

                var expense =
                    rawStats
                        .FirstOrDefault(x => x.Month == i && x.Type == TransactionType.Expense)
                        ?.Total
                    ?? 0m;

                monthlyStats.Add(
                    new MonthlyStatResponse($"{request.Year}-{i:D2}", income, expense)
                );
            }

            return new MonthlyStatsResponse(monthlyStats, request.Currency);
        }
    }
}
