using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(x => x.Id);

        // One-to-Many with encapsulation support
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("CartId").IsRequired() // Shadow Property in CartItem table
            .OnDelete(DeleteBehavior.Cascade);

        // Tell EF to use the private backing field _items
        var navigation = builder.Metadata.FindNavigation(nameof(Cart.Items));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}