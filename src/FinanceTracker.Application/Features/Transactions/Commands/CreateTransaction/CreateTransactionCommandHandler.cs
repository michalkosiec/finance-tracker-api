using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<CreateTransactionCommand, TransactionResponse>
    {
        public async Task<TransactionResponse> Handle(
            CreateTransactionCommand request,
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
                ?? throw new NotFoundException(
                    nameof(Budget),
                    new { request.CategoryId, Month = month }
                );

            // Change this when adding proper currency handling
            var query = context.Transactions.Where(t =>
                t.UserId == request.UserId && t.CategoryId == request.CategoryId && t.Month == month
            );

            var totalMonthlyExpense = await query
                .Where(t => t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            var totalMonthlyIncome = await query
                .Where(t => t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            Money currentBallance = new(totalMonthlyIncome - totalMonthlyExpense, request.Currency);

            budget.VerifySufficientFunds(amount, currentBallance);

            var transaction = Transaction.Create(
                request.UserId,
                request.Name,
                amount,
                request.CategoryId,
                request.Date,
                request.Type
            );

            await context.Transactions.AddAsync(transaction, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<TransactionResponse>(transaction);
        }
    }
}
