using FinanceTracker.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IAppDbContext _context;

        public CreateCategoryCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must be at most 100 characters long.")
                .MustAsync(BeUniqueNameForUser)
                .WithMessage("Category name must be unique");

            RuleFor(x => x.Icon)
                .MaximumLength(100)
                .WithMessage("Icon must be at most 100 characters long.");

            RuleFor(x => x.Color)
                .MaximumLength(20)
                .WithMessage("Color must be at most 20 characters long.");
        }

        private async Task<bool> BeUniqueNameForUser(
            CreateCategoryCommand command,
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
