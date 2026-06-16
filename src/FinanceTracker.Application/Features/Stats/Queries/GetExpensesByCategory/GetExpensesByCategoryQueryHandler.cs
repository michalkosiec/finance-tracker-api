using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Stats.Queries.GetExpensesByCategory
{
    public class GetExpensesByCategoryQueryHandler(IAppDbContext context)
        : IRequestHandler<GetExpensesByCategoryQuery, ExpensesByCategoryResponse>
    {
        public async Task<ExpensesByCategoryResponse> Handle(
            GetExpensesByCategoryQuery request,
            CancellationToken cancellationToken
        )
        {
            var targetDate = new DateTime(
                request.Year,
                request.Month,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc
            );
            var query =
                from t in context.Transactions
                where
                    t.UserId == request.UserId
                    && t.Month == targetDate
                    && t.Type == TransactionType.Expense
                    && t.Amount.Currency == request.Currency
                join c in context.Categories on t.CategoryId equals c.Id
                select new { CategoryName = c.Name, DecimalAmount = t.Amount.Amount } into flattened
                group flattened by flattened.CategoryName into groupedData
                select new
                {
                    CategoryName = groupedData.Key,
                    TotalExpense = groupedData.Sum(x => x.DecimalAmount),
                    Count = groupedData.Count(),
                };

            var result = await query
                .OrderByDescending(x => x.TotalExpense)
                .ToListAsync(cancellationToken);

            var categoryStats = result
                .Select(x => new CategoryStatResponse(x.CategoryName, x.TotalExpense, x.Count))
                .ToList();

            return new ExpensesByCategoryResponse(categoryStats, request.Currency);
        }
    }
}
