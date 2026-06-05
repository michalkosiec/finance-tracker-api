using FluentValidation;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(q => q.UserId).NotEmpty().WithMessage("User ID is required.");

            RuleFor(q => q.CategoryId).NotEmpty().WithMessage("Category ID is required.");
        }
    }
}
