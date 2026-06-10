using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category =
                context.Categories.FirstOrDefault(c =>
                    c.Id == request.Id && c.UserId == request.UserId
                ) ?? throw new NotFoundException(nameof(Category), new { request.Id });

            category.UpdateName(request.Name);
            category.UpdateIcon(request.Icon);
            category.UpdateColor(request.Color);

            context.Categories.Update(category);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
