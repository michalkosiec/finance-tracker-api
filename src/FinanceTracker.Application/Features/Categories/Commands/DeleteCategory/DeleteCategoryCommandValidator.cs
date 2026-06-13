using FluentValidation;

namespace FinanceTracker.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid category ID is required.");
        }
    }
}
