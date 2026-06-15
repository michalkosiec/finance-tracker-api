using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget
{
    public record UpdateBudgetCommand(
        Guid Id,
        Guid UserId,
        Guid CategoryId,
        decimal LimitAmount,
        string Currency,
        string Month
    ) : IRequest<Unit> { }
}
