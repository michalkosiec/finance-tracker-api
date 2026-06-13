using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget
{
    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Valid ID is required.");

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid category ID is required.");

            RuleFor(x => x.LimitAmount)
                .GreaterThan(0)
                .WithMessage("Limit amount must be a positive value.");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3)
                .WithMessage("Currency must be a valid 3-character code.")
                .Must(c => c.All(char.IsLetter))
                .WithMessage("Currency must contain only letters.");

            RuleFor(x => x.Month)
                .NotEmpty()
                .WithMessage("Month is required.")
                .Must(m => m.Day == 1)
                .WithMessage("Month must be the first day of the month.");
        }
    }
}
