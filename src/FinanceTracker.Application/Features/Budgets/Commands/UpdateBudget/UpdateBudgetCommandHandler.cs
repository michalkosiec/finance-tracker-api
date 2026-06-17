using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Budgets.Commands.UpdateBudget
{
    public class UpdateBudgetCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateBudgetCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateBudgetCommand request,
            CancellationToken cancellationToken
        )
        {
            var budget =
                context.Budgets.FirstOrDefault(b =>
                    b.UserId == request.UserId && b.Id == request.Id
                ) ?? throw new NotFoundException(nameof(Budget), new { request.Id });

            var query = context.Transactions.Where(t =>
                t.UserId == request.UserId
                && t.CategoryId == request.CategoryId
                && t.Month == budget.Month
            );

            var totalMonthlyExpense = await query
                .Where(t => t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            var totalMonthlyIncome = await query
                .Where(t => t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount.Amount, cancellationToken);

            Money currentBallance = new(totalMonthlyIncome - totalMonthlyExpense, request.Currency);

            budget.UpdateLimitAmount(
                new Money(request.LimitAmount, request.Currency),
                currentBallance
            );

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
