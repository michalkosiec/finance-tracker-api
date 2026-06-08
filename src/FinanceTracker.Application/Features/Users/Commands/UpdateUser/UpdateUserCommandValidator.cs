using FluentValidation;

namespace FinanceTracker.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
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
                .WithMessage("Email must not exceed 200 characters.");
        }
    }
}
