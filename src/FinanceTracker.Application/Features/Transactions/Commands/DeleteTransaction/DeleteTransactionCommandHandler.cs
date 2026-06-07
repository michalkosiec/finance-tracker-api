using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler(IAppDbContext context)
        : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteTransactionCommand request,
            CancellationToken cancellationToken
        )
        {
            var transaction =
                await context.Transactions.FirstOrDefaultAsync(
                    t => t.UserId == request.UserId && t.Id == request.TransactionId,
                    cancellationToken
                )
                ?? throw new KeyNotFoundException(
                    "Transaction not found for the given user ID and transaction ID."
                );

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
