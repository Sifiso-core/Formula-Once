using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ProductId)
            .IsRequired();
        
        builder.Property(x => x.PriceAtPurchase)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.Property<Guid>("OrderId")
            .IsRequired();
    }
}