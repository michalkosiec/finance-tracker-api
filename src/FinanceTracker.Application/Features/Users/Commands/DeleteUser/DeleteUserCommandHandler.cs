using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IAppDbContext context)
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var transactions = await context
                .Transactions.Where(t => t.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var budgets = await context
                .Budgets.Where(b => b.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var categories = await context
                .Categories.Where(c => c.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var user =
                await context.Users.FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken
                ) ?? throw new KeyNotFoundException("User not found for the given user ID.");

            context.Transactions.RemoveRange(transactions);
            context.Budgets.RemoveRange(budgets);
            context.Categories.RemoveRange(categories);
            context.Users.Remove(user);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
