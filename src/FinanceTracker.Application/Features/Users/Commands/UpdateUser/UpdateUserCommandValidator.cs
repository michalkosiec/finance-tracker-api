using FinanceTracker.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email address format is required.")
                .MaximumLength(200)
                .WithMessage("Email must not exceed 200 characters.")
                .MustAsync(BeUniqueEmail)
                .WithMessage("Email must be unique.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
