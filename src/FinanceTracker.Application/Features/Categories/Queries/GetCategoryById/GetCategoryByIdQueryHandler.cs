using AutoMapper;
using AutoMapper.QueryableExtensions;
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
             var categoryResponse =
                await context
                    .Categories.Where(c => c.Id == request.CategoryId && c.UserId == request.UserId,)
                    .ProjectTo<CategoryResponse>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException("User not found for the given category and user ID.");

            return categoryResponse;
        }
    }
}
