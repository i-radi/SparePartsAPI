using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpareParts.Data.Models;

namespace SpareParts.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(b => b.OrderNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(b => b.TotalOrderPrice)
            .HasMaxLength(10)
            .IsRequired();
    }
} 