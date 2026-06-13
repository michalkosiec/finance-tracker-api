using FinanceTracker.Domain.Entities;
using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Transaction name is required.")
                .MaximumLength(100)
                .WithMessage("Transaction name must not exceed 100 characters.");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be a positive value.");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3)
                .WithMessage("Currency must be a valid 3-character code.")
                .Must(c => c.All(char.IsLetter))
                .WithMessage("Currency must contain only letters.");

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid category ID is required.");

            RuleFor(v => v.Date).NotEmpty().WithMessage("Date is required.");

            RuleFor(v => v.Type)
                .Must(t => t == TransactionType.Income || t == TransactionType.Expense)
                .WithMessage("The transaction type must be either Income or Expense.");
        }
    }
}
