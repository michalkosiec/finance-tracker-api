using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Categories;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        public async Task<CategoryResponse> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            Category category = Category.Create(
                request.UserId,
                request.Name,
                request.Icon,
                request.Color
            );

            context.Categories.Add(category);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<CategoryResponse>(category);
        }
    }
}
