using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).HasMaxLength(100).IsRequired();

            // 1. Map the Value Object
            builder.ComplexProperty(
                t => t.Amount,
                amountBuilder =>
                {
                    amountBuilder
                        .Property(m => m.Amount)
                        .HasColumnName("Amount")
                        .HasPrecision(18, 2)
                        .IsRequired();

                    amountBuilder
                        .Property(m => m.Currency)
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsRequired();
                }
            );

            builder
                .Property(t => t.Month)
                .HasComputedColumnSql("date_trunc('month', \"Date\")", stored: true);

            builder.Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
        }
    }
}
