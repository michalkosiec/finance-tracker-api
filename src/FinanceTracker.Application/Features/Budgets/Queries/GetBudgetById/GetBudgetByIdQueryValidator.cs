using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryValidator : AbstractValidator<GetBudgetByIdQuery>
    {
        public GetBudgetByIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.BudgetId).NotEmpty().WithMessage("BudgetId is required.");
        }
    }
}
