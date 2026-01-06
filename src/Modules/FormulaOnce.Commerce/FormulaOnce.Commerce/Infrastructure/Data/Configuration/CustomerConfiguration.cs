using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Commerce.Infrastructure.Data.Configuration;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.Id);

        // Map Shipping Address with specific column prefixes
        builder.OwnsOne(x => x.DefaultShippingAddress, sa =>
        {
            sa.Property(p => p.Street).HasColumnName("ShippingStreet");
            sa.Property(p => p.City).HasColumnName("ShippingCity");
            sa.Property(p => p.Province).HasColumnName("ShippingState");
            sa.Property(p => p.PostalCode).HasColumnName("ShippingZipCode");
            sa.Property(p => p.Country).HasColumnName("ShippingCountry");
        });

        // Map Billing Address with specific column prefixes
        builder.OwnsOne(x => x.DefaultBillingAddress, ba =>
        {
            ba.Property(p => p.Street).HasColumnName("BillingStreet");
            ba.Property(p => p.City).HasColumnName("BillingCity");
            ba.Property(p => p.Province).HasColumnName("BillingState");
            ba.Property(p => p.PostalCode).HasColumnName("BillingZipCode");
            ba.Property(p => p.Country).HasColumnName("BillingCountry");
        });
    }
}