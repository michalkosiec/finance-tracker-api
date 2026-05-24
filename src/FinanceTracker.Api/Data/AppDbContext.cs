using Microsoft.EntityFrameworkCore;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Budget> Budgets { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Budget>()
                .HasIndex(b => new { b.UserId, b.CategoryId, b.Month })
                .IsUnique();
        }
    }
}