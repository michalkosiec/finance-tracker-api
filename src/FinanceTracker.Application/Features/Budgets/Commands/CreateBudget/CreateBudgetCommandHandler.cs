using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Common.DTOs.Budgets;
using MediatR;
using AutoMapper;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.ValueObjects;

namespace FinanceTracker.Application.Features.Budgets.Commands.CreateBudget
{
    public class CreateBudgetCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateBudgetCommand, BudgetResponse>
    {
        public async Task<BudgetResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = Budget.Create(
                request.UserId,
                request.CategoryId,
                new Money(request.LimitAmount, request.Currency),
                request.Month
            );

            context.Budgets.Add(budget);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<BudgetResponse>(budget);
        }
    }
}