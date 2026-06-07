using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId is required");
        }
    }
}
