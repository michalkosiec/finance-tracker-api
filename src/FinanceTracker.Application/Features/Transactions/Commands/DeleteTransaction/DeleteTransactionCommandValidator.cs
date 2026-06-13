using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.TransactionId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid transaction iD is required.");
        }
    }
}
