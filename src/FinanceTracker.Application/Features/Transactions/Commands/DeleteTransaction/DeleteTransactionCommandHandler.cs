using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
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
                ?? throw new NotFoundException(nameof(Transaction), new { request.TransactionId });

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
