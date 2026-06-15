using FinanceTracker.Application.Common.Exceptions;
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
            var transactionDate = DateTime.ParseExact(
                request.Date,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture
            );

            DateTime month = new(
                transactionDate.Year,
                transactionDate.Month,
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
                ?? throw new NotFoundException(
                    nameof(Budget),
                    new { request.CategoryId, Month = month }
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
                ) ?? throw new NotFoundException(nameof(Transaction), new { request.Id });

            transaction.UpdateName(request.Name);
            transaction.UpdateAmount(new Money(request.Amount, request.Currency));
            transaction.UpdateCategory(request.CategoryId);
            transaction.UpdateDate(transactionDate);
            transaction.UpdateType(request.Type);

            context.Transactions.Update(transaction);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
