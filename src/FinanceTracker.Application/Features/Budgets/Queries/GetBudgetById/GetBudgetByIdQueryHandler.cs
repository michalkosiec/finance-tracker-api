using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetBudgetByIdQuery, BudgetResponse>
    {
        public async Task<BudgetResponse> Handle(
            GetBudgetByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var budgetResponse =
                await context
                    .Budgets.Where(b => b.Id == request.BudgetId && b.UserId == request.UserId)
                    .ProjectTo<BudgetResponse>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException(
                    "Budget not found for the given budget and user ID."
                );

            return budgetResponse;
        }
    }
}
