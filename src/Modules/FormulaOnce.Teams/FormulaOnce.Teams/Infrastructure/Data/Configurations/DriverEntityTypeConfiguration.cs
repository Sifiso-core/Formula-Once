using FormulaOnce.Teams.Domain.Driver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Teams.Infrastructure.Data.Configurations;

internal class DriverEntityTypeConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable(DbConstants.DriversTable);

        // Primary Key
        builder.HasKey(x => x.Id);

        // Basic Properties
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Nationality).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Acronym).IsRequired().HasMaxLength(3);
        builder.Property(x => x.DateOfBirth).IsRequired();

        // Foreign Key Relationship (The fix for ConstructorId1)
        builder.HasOne(d => d.Constructor)
            .WithMany(c => c.Drivers)
            .HasForeignKey(d => d.ConstructorId) // Explicitly use the property in your class
            .OnDelete(DeleteBehavior.Restrict);

        // Map DriverStats as an Owned Entity in its own table
        builder.OwnsOne(d => d.CareerDriverStats, stats =>
        {
            stats.ToTable(DbConstants.DriverCareerStats);
            // Ensure EF core knows the owner's ID is the PK for this table too
            stats.WithOwner().HasForeignKey("DriverId");
        });
    }
}