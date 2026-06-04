using FinanceTracker.Application.Common.Interfaces;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandler(IAppDbContext context) : IRequestHandler<DeleteBudgetCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = context.Budgets.FirstOrDefault(b => b.Id == request.BudgetId
                && b.UserId == request.UserId)
                ?? throw new KeyNotFoundException("Budget not found for the specified user and budget ID.");

            context.Budgets.Remove(budget);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}