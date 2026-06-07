using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetTransactionByIdQuery, TransactionResponse>
    {
        public async Task<TransactionResponse> Handle(
            GetTransactionByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(
                t => t.UserId == request.UserId && t.Id == request.TransactionId,
                cancellationToken
            );

            return mapper.Map<TransactionResponse>(transaction);
        }
    }
}
