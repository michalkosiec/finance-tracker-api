using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Budgets;
using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.ValueObjects;
using MediatR;

namespace FinanceTracker.Application.Features.Budgets.Commands.CreateBudget
{
    public class CreateBudgetCommandHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<CreateBudgetCommand, BudgetResponse>
    {
        public async Task<BudgetResponse> Handle(
            CreateBudgetCommand request,
            CancellationToken cancellationToken
        )
        {
            if (
                !DateTime.TryParseExact(
                    request.Month,
                    "yyyy-MM",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out var monthDate
                )
            )
            {
                throw new BadRequestException(
                    "The provided month is invalid. Please use 'yyyy-MM'."
                );
            }

            var budget = Budget.Create(
                request.UserId,
                request.CategoryId,
                new Money(request.LimitAmount, request.Currency),
                monthDate
            );

            context.Budgets.Add(budget);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<BudgetResponse>(budget);
        }
    }
}
