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
            var category =
                await context.Categories.FirstOrDefaultAsync(c =>
                    c.Id == request.Id && c.UserId == request.UserId
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
