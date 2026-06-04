using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetsQuery
{
    public class GetBudgetsQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBudgetsQuery, IEnumerable<BudgetResponse>>
    {
        public async Task<IEnumerable<BudgetResponse>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            return await context.Budgets.Where(b => b.UserId == request.UserId)
                .ProjectTo<BudgetResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}