using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Queries.GetTransactions
{
    public class GetTransactionsQueryHandler(IAppDbContext context, IMapper mapper)
        : IRequestHandler<GetTransactionsQuery, IReadOnlyCollection<TransactionResponse>>
    {
        public async Task<IReadOnlyCollection<TransactionResponse>> Handle(
            GetTransactionsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await context
                .Transactions.Where(t => t.UserId == request.UserId)
                .ProjectTo<TransactionResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
