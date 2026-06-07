using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(IAppDbContext context)
        : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var isReferenced =
                await context.Budgets.AnyAsync(
                    b => b.CategoryId == request.CategoryId,
                    cancellationToken
                )
                || await context.Transactions.AnyAsync(
                    t => t.CategoryId == request.CategoryId,
                    cancellationToken
                );

            if (isReferenced)
                throw new InvalidOperationException(
                    "Cannot delete this category because it is associated with existing budget or transaction"
                );

            var category =
                await context.Categories.FirstOrDefaultAsync(
                    c => c.Id == request.CategoryId && c.UserId == request.UserId,
                    cancellationToken
                )
                ?? throw new KeyNotFoundException(
                    "Category not found for the specified category ID."
                );

            context.Categories.Remove(category);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
