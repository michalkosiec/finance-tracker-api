using FinanceTracker.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IAppDbContext _context;

        public CreateUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.IdentityUserId)
                .NotEmpty()
                .WithMessage("The Identity User ID is required to link the account.");

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
