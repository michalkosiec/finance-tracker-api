using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Categories;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        public async Task<CategoryResponse> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var category =
                await context
                    .Categories.AsNoTracking()
                    .FirstOrDefaultAsync(
                        c => c.Id == request.CategoryId && c.UserId == request.UserId,
                        cancellationToken
                    )
                ?? throw new KeyNotFoundException(
                    "Category not found for the specified category ID."
                );

            return mapper.Map<CategoryResponse>(category);
        }
    }
}
