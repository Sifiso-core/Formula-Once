using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(false);


        builder.Property(x => x.Type)
            .HasConversion(new EnumToStringConverter<ProductType>())
            .HasMaxLength(30);

        builder.OwnsOne(x => x.Price, p =>
        {
            p.Property(a => a.Amount)
                .HasColumnName("Price")
                .HasPrecision(18, 2);

            p.Property(c => c.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3);
        });
    }
}