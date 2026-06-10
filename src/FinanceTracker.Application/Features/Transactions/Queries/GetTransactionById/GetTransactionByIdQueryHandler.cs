using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Application.Common.Exceptions;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
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
            var transactionResponse =
                await context
                    .Transactions.Where(t =>
                        t.UserId == request.UserId && t.Id == request.TransactionId
                    )
                    .ProjectTo<TransactionResponse>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(Transaction), new { request.TransactionId });

            return transactionResponse;
        }
    }
}
