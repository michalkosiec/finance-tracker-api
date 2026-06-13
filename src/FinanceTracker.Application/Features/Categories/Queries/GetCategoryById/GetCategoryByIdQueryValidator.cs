using FluentValidation;

namespace FinanceTracker.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The request does not contain a valid User ID.");

            RuleFor(q => q.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Valid category ID is required.");
        }
    }
}
