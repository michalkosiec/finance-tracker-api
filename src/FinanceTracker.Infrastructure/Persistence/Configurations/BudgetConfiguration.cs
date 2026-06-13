using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations
{
    public class BudgetConfiguration() : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("Budgets");

            builder.HasKey(b => b.Id);

            builder.ComplexProperty(
                b => b.LimitAmount,
                limitBuilder =>
                {
                    limitBuilder
                        .Property(m => m.Amount)
                        .HasColumnName("LimitAmount")
                        .HasPrecision(18, 2)
                        .IsRequired();

                    limitBuilder
                        .Property(m => m.Currency)
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsRequired();
                }
            );

            builder.Property(b => b.Month).IsRequired();
        }
    }
}
