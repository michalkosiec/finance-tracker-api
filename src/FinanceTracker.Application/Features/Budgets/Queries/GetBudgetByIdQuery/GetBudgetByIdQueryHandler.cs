using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetByIdQuery
{
    public class GetBudgetByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBudgetByIdQuery, BudgetResponse>
    {
        public async Task<BudgetResponse> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            var budget = context.Budgets.FirstOrDefault(b => b.Id == request.BudgetId
                && b.UserId == request.UserId)
                ?? throw new KeyNotFoundException("Budget not found for the specified user and budget ID.");

            return mapper.Map<BudgetResponse>(budget);
        }
    }
}