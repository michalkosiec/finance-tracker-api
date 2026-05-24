using System.Globalization;
using FinanceTracker.Api.Dtos.Budgets;
using FinanceTracker.Api.Dtos.Stats;
using FinanceTracker.Api.Models;
using AutoMapper;

namespace FinanceTracker.Api.Profiles
{
    public class StatsProfile : Profile
    {
        public StatsProfile()
        {
            CreateMap<CategoryStats, CategoryStatsReadDto>();

            CreateMap<MonthlyStats, MonthlyStatsReadDto>();
        }
    }
}