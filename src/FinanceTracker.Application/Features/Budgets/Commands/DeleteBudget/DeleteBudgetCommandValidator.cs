using FluentValidation;

namespace FinanceTracker.Application.Features.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
    {
        public DeleteBudgetCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.BudgetId).NotEmpty().WithMessage("BudgetId is required.");
        }
    }
}
