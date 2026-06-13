using FluentValidation;

namespace FinanceTracker.Application.Features.Stats.Queries.GetSummary
{
    public class GetSummaryQueryValidator : AbstractValidator<GetSummaryQuery>
    {
        public GetSummaryQueryValidator()
        {
            RuleFor(x => x.Year).GreaterThan(1990).WithMessage("Year must be greater than 1990.");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .WithMessage("Currency is required.")
                .Length(3)
                .WithMessage("Currency must be exactly 3 characters.")
                .Matches("^[A-Z]{3}$")
                .WithMessage("Currency must be a 3-letter uppercase ISO code (e.g., USD, EUR).");

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");
        }
    }
}
