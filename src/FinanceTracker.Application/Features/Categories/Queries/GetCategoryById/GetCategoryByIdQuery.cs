using FinanceTracker.Application.Common.DTOs.Categories;
using MediatR;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid UserId, Guid CategoryId)
        : IRequest<CategoryResponse> { }
}
