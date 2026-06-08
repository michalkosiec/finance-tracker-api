using FluentValidation;

namespace FinanceTracker.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        }
    }
}
