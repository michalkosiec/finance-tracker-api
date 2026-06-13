using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryValidator : AbstractValidator<GetBudgetByIdQuery>
    {
        public GetBudgetByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.BudgetId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid budget ID is required.");
        }
    }
}
