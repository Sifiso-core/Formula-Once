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

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CartId).IsRequired();

        builder.HasOne(c => c.Cart).WithMany(c => c.Items).HasForeignKey(c => c.CartId).HasPrincipalKey(c => c.Id);
    }
}