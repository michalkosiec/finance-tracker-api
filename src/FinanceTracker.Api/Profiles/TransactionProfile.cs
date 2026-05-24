using FinanceTracker.Api.Models;
using FinanceTracker.Api.Dtos.Transactions;
using System.Globalization;

namespace FinanceTracker.Api.Profiles
{
    public class TransactionProfile : AutoMapper.Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionReadDto>();

            CreateMap<TransactionCreateDto, Transaction>().ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        ))).ForMember(dest => dest.UserId, opt => opt.Ignore());;

            CreateMap<TransactionUpdateDto, Transaction>().ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(
            DateTime.ParseExact(src.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture), 
            DateTimeKind.Utc
        ))).ForMember(dest => dest.UserId, opt => opt.Ignore());;

            CreateMap<TransactionQueryDto, TransactionParameters>();
        }
    }
}