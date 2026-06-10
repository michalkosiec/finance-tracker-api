using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandler(IAppDbContext context)
        : IRequestHandler<DeleteBudgetCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteBudgetCommand request,
            CancellationToken cancellationToken
        )
        {
            var budget =
                context.Budgets.FirstOrDefault(b =>
                    b.Id == request.BudgetId && b.UserId == request.UserId
                ) ?? throw new NotFoundException(nameof(Budget), new { request.BudgetId });

            context.Budgets.Remove(budget);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
