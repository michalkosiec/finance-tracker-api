using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateTransactionCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateTransactionCommand request,
            CancellationToken cancellationToken
        )
        {
            DateTime month = new(
                request.Date.Year,
                request.Date.Month,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc
            );

            Money amount = new(request.Amount, request.Currency);

            var budget =
                await context.Budgets.FirstOrDefaultAsync(
                    b =>
                        b.UserId == request.UserId
                        && b.CategoryId == request.CategoryId
                        && b.Month == month,
                    cancellationToken
                )
                ?? throw new KeyNotFoundException(
                    "Budget not found for the specified category ID and month."
                );
            ;

            // Change this when adding proper currency handling
            var query = context.Transactions.Where(t =>
                t.UserId == request.UserId
                && t.CategoryId == request.CategoryId
                && t.Month == month
                && t.Id != request.Id
            );

            var totalMonthlyExpense = await query
                .Where(t => t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            var totalMonthlyIncome = await query
                .Where(t => t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            Money currentBallance = new(totalMonthlyIncome - totalMonthlyExpense, request.Currency);

            budget.VerifySufficientFunds(amount, currentBallance);

            var transaction =
                await context.Transactions.FirstOrDefaultAsync(
                    t => t.UserId == request.UserId && t.Id == request.Id,
                    cancellationToken
                )
                ?? throw new KeyNotFoundException(
                    "Transaction not found for the given user and transaction ID."
                );

            transaction.UpdateName(request.Name);
            transaction.UpdateAmount(new Money(request.Amount, request.Currency));
            transaction.UpdateCategory(request.CategoryId);
            transaction.UpdateDate(request.Date);
            transaction.UpdateType(request.Type);

            context.Transactions.Update(transaction);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
