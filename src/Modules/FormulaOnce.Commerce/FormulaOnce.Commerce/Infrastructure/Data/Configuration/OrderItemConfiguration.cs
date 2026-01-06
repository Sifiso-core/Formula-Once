using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // 1. Table and Key
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.Id);

        // 2. Product Link
        // We store the Guid. We don't necessarily need a hard Foreign Key 
        // to the Product table if they are in different bounded contexts, 
        // but for a single module, it's fine.
        builder.Property(x => x.ProductId)
            .IsRequired();

        // 3. Price Snapshot (Historical Accuracy)
        builder.Property(x => x.PriceAtPurchase)
            .HasPrecision(18, 2)
            .IsRequired();

        // 4. Quantity Guard
        builder.Property(x => x.Quantity)
            .IsRequired();

        // 5. Relationship (The "Shadow Property")
        // This links the OrderItem back to the Order without 
        // needing a 'public Guid OrderId' property in the OrderItem class.
        builder.Property<Guid>("OrderId")
            .IsRequired();
    }
}