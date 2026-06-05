using FluentValidation;

namespace FinanceTracker.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        }
    }
}
