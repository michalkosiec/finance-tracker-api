using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Domain.ValueObjects;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.CreateBudget
{
    public record CreateBudgetCommand(
        Guid UserId,
        Guid CategoryId,
        decimal LimitAmount,
        string Currency,
        string Month
    ) : IRequest<BudgetResponse> { }
}
