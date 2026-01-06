using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        // Explicitly define the shadow property here as well
        builder.Property<Guid>("CartId").IsRequired();
    }
}