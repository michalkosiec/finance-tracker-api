using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(x => x.TransactionId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid transaction ID is required.");
        }
    }
}
