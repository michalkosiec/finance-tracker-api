using System.Reflection;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options),
            IAppDbContext
    {
        public DbSet<Budget> Budgets => Set<Budget>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
