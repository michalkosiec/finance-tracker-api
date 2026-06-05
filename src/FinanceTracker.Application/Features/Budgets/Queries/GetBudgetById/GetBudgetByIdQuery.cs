using FinanceTracker.Application.Common.DTOs.Budgets;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetById
{
    public record GetBudgetByIdQuery(Guid UserId, Guid BudgetId) : IRequest<BudgetResponse> { }
}
