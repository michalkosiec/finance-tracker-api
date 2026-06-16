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

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Name).HasMaxLength(100).IsRequired();

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
                .HasComputedColumnSql(
                    "date_trunc('month', \"Date\" AT TIME ZONE 'UTC')",
                    stored: true
                );

            builder.Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
        }
    }
}
