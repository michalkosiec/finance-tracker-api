using MediatR;

namespace FinanceTracker.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        Guid Id,
        Guid UserId,
        string Name,
        string Icon,
        string Color
    ) : IRequest<Unit> { };
}
