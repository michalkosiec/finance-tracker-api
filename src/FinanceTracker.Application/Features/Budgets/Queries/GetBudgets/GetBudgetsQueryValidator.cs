using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Queries.GetBudgets
{
    public class GetBudgetsQueryValidator : AbstractValidator<GetBudgetsQuery>
    {
        public GetBudgetsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");
        }
    }
}
