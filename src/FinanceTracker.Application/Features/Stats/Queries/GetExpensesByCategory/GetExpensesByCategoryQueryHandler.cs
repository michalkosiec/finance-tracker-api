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
            var query =
                from t in context.Transactions
                where
                    t.UserId == request.UserId
                    && t.Date.Year == request.Year
                    && t.Date.Month == request.Month
                    && t.Type == TransactionType.Expense
                    && t.Amount.Currency == request.Currency
                join c in context.Categories on t.CategoryId equals c.Id
                group t by c.Name into groupedData
                select new CategoryStatResponse(
                    groupedData.Key,
                    groupedData.Sum(x => x.Amount.Amount),
                    groupedData.Count()
                );

            var categoryStats = await query
                .OrderByDescending(x => x.TotalExpense)
                .ToListAsync(cancellationToken);

            return new ExpensesByCategoryResponse(categoryStats, request.Currency);
        }
    }
}
