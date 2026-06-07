using MediatR;

namespace FinanceTracker.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid UserId, Guid CategoryId) : IRequest<Unit>;
}
