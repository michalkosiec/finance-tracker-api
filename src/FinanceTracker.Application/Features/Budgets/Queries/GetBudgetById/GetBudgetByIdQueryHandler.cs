using AutoMapper;
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
            var budget =
                context
                    .Budgets.AsNoTracking()
                    .FirstOrDefault(b => b.Id == request.BudgetId && b.UserId == request.UserId)
                ?? throw new KeyNotFoundException(
                    "Budget not found for the specified user and budget ID."
                );

            return mapper.Map<BudgetResponse>(budget);
        }
    }
}
