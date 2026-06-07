using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
