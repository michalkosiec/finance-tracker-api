using FinanceTracker.Application.Common.DTOs.Budgets;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetsQuery
{
    public record GetBudgetsQuery(Guid UserId) : IRequest<IEnumerable<BudgetResponse>>
    { }
}