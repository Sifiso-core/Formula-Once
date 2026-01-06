using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        // Value Object: Flatten Address into the Order table
        builder.OwnsOne(x => x.ShippingAddress, sa =>
        {
            sa.Property(p => p.Street).HasMaxLength(200).IsRequired();
            sa.Property(p => p.City).HasMaxLength(100).IsRequired();
            sa.Property(p => p.Country).HasMaxLength(100).IsRequired();
        });

        builder.Property(x => x.Status)
            .HasConversion(new EnumToStringConverter<OrderStatus>())
            .HasMaxLength(20);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Restrict);

        var navigation = builder.Metadata.FindNavigation(nameof(Order.Items));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}