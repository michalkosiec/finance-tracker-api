using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgets
{
    public class GetBudgetsQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetBudgetsQuery, IReadOnlyCollection<BudgetResponse>>
    {
        public async Task<IReadOnlyCollection<BudgetResponse>> Handle(
            GetBudgetsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await context
                .Budgets.AsNoTracking()
                .Where(b => b.UserId == request.UserId)
                .ProjectTo<BudgetResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
