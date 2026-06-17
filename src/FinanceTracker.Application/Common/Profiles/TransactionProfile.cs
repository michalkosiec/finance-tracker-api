using AutoMapper;
using FinanceTracker.Application.Common.DTOs.Transactions;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponse>()
                .ForCtorParam("Amount", opt => opt.MapFrom(src => src.Amount.Amount))
                .ForCtorParam("Currency", opt => opt.MapFrom(src => src.Amount.Currency))
                .ForCtorParam("Date", opt => opt.MapFrom(src => src.Date));
        }
    }
}
