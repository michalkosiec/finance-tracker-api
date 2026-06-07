using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId is required.");
        }
    }
}
