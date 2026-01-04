using FormulaOnce.Events.Domain.Circuit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Events.Infrastructure.Data.Configuration;

public class CircuitEntityTypeConfiguration : IEntityTypeConfiguration<Circuit>
{
    public void Configure(EntityTypeBuilder<Circuit> builder)
    {
        builder.ToTable("Circuits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.OwnsOne(x => x.Location, l =>
        {
            l.Property(p => p.Country).HasColumnName(DbConstants.LocationConstants.CountryColumnName).HasMaxLength(50);

            l.Property(p => p.City).HasColumnName(DbConstants.LocationConstants.CityColumnName).HasMaxLength(50);

            l.OwnsOne(p => p.Coordinates, c =>
            {
                c.Property(p => p.Latitude).HasColumnName(DbConstants.LocationConstants.LatitudeColumnName);

                c.Property(p => p.Longitude).HasColumnName(DbConstants.LocationConstants.LongitudeColumnName);
            });
        });


        builder.OwnsOne(x => x.LapRecord, r =>
        {
            r.ToTable(DbConstants.LapRecordConstants.TableName); // Separate table for normalization

            r.Property(p => p.Time).HasColumnName(DbConstants.LapRecordConstants.TimeColumnName);

            r.Property(p => p.DriverName).HasColumnName(DbConstants.LapRecordConstants.DriverColumnName);
        });


        builder.OwnsMany(x => x.Landmarks, l =>
        {
            l.ToTable(DbConstants.CircuitLandmarkConstants.TableName);

            l.WithOwner().HasForeignKey("CircuitId");

            l.HasKey("Id");

            l.Property(p => p.Label).HasMaxLength(100);

            l.Property(p => p.LandmarkType).HasConversion<string>();
        });
    }
}