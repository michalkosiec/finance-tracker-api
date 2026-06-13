using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
    {
        public DeleteBudgetCommandValidator()
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
