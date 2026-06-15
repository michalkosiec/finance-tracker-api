using FinanceTracker.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateCategoryCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.")
                .MustAsync(BeUniqueNameForUser)
                .WithMessage("Category name must be unique.");

            RuleFor(x => x.Icon)
                .NotEmpty()
                .WithMessage("Icon is required.")
                .MaximumLength(50)
                .WithMessage("Icon cannot exceed 50 characters.");

            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Color is required.")
                .MaximumLength(20)
                .WithMessage("Color must be at most 20 characters long.");
        }

        private async Task<bool> BeUniqueNameForUser(
            UpdateCategoryCommand command,
            string name,
            CancellationToken cancellationToken
        )
        {
            bool exists = await _context.Categories.AnyAsync(
                c => c.UserId == command.UserId && c.Name == name,
                cancellationToken
            );

            return !exists;
        }
    }
}
