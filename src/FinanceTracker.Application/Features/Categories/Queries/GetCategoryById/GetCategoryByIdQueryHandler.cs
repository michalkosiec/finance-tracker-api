using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Categories;
using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
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
                ?? throw new NotFoundException(nameof(Category), new { request.CategoryId });

            return categoryResponse;
        }
    }
}
