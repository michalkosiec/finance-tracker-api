using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Categories;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<CategoryResponse>>
    {
        public async Task<IReadOnlyCollection<CategoryResponse>> Handle(
            GetCategoriesQuery request,
            CancellationToken cancellationToken
        )
        {
            return await context
                .Categories.AsNoTracking()
                .Where(c => c.UserId == request.UserId)
                .ProjectTo<CategoryResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
