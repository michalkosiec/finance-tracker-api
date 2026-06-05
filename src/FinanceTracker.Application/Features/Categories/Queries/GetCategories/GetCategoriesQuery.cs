using FinanceTracker.Application.Common.DTOs.Categories;
using MediatR;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery(Guid UserId)
        : IRequest<IReadOnlyCollection<CategoryResponse>> { };
}
