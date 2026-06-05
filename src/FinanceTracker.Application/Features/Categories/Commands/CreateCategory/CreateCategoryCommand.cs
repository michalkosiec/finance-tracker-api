using FinanceTracker.Application.Common.DTOs.Categories;
using MediatR;

namespace FinanceTracker.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(Guid UserId, string Name, string Icon, string Color)
        : IRequest<CategoryResponse> { }
}
