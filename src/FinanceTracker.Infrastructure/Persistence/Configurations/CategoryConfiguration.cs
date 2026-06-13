using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Icon).HasMaxLength(50).IsRequired();

            builder.Property(c => c.Color).HasMaxLength(20).IsRequired();

            builder.HasIndex(c => new { c.UserId, c.Name }).IsUnique();
        }
    }
}
