using FinanceTracker.Domain.ValueObjects;
using FinanceTracker.Application.Common.DTOs.Budgets;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.CreateBudget
{
    public record CreateBudgetCommand(Guid UserId, Guid CategoryId, decimal LimitAmount, string Currency, DateTime Month) : IRequest<BudgetResponse>
    {
    }
}