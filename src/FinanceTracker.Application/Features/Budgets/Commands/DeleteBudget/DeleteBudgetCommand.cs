using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget
{
    public record DeleteBudgetCommand(Guid UserId, Guid BudgetId) : IRequest<Unit>
    { }
}