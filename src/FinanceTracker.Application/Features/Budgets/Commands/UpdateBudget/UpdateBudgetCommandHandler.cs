using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.ValueObjects;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget
{
    public class UpdateBudgetCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateBudgetCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateBudgetCommand request,
            CancellationToken cancellationToken
        )
        {
            var budget =
                context.Budgets.FirstOrDefault(b =>
                    b.UserId == request.UserId && b.Id == request.Id
                )
                ?? throw new KeyNotFoundException(
                    "Budget not found for the specified user and budget ID."
                );

            budget.UpdateLimitAmount(new Money(request.LimitAmount, request.Currency));
            budget.UpdateCategory(request.CategoryId);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
