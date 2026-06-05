using FinanceTracker.Application.Common.DTOs.Budgets;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgets
{
    public record GetBudgetsQuery(Guid UserId) : IRequest<IReadOnlyCollection<BudgetResponse>> { }
}
