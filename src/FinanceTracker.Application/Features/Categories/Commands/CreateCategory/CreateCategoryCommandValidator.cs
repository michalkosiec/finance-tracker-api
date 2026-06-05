using FluentValidation;

namespace FinanceTracker.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must be at most 100 characters long.");

            RuleFor(x => x.Icon)
                .MaximumLength(100)
                .WithMessage("Icon must be at most 100 characters long.");

            RuleFor(x => x.Color)
                .MaximumLength(20)
                .WithMessage("Color must be at most 20 characters long.");
        }
    }
}
