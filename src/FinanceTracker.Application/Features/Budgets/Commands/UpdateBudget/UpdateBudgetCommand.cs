using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget
{
    public record UpdateBudgetCommand(
        Guid UserId,
        Guid CategoryId,
        decimal LimitAmount,
        string Currency,
        DateTime Month
    ) : IRequest<Unit> { }
}
