using FormulaOnce.Teams.Domain.Constructor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Teams.Infrastructure.Data.Configurations;

internal class ConstructorEntityTypeConfiguration : IEntityTypeConfiguration<Constructor>
{
    public void Configure(EntityTypeBuilder<Constructor> builder)
    {
        builder.ToTable(DbConstants.ConstructorConstants.ConstructorsTable);

        builder.HasKey(x => x.Id);

        // Basic Properties
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.BaseLocation).IsRequired().HasMaxLength(200);

        // Map Drivers via backing field
        builder.HasMany(x => x.Drivers)
            .WithOne(d => d.Constructor) // Reference the navigation property in Driver
            .HasForeignKey(d => d.ConstructorId) // Use the existing Guid property
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        //builder.Navigation(x => x.Drivers).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

        // 1. Map ConstructorStats to its own table
        builder.OwnsOne(x => x.Stats, stats =>
        {
            stats.ToTable(DbConstants.ConstructorConstants.ConstructorStatisticsTable);

            // 2. Map AllTimeSummary (Nested table)
            stats.OwnsOne(s => s.AllTimeSummary, allTime =>
            {
                allTime.ToTable(DbConstants.ConstructorConstants.ConstructorAllTimeStatsTable);
                allTime.Property(a => a.HighestRaceFinish).HasMaxLength(50);
                allTime.Property(a => a.HighestGridPosition).HasMaxLength(50);
            });

            // 3. Map SeasonStats (Nested table)
            stats.OwnsOne(s => s.SeasonStats, season =>
            {
                season.ToTable(DbConstants.ConstructorConstants.ConstructorSeasonStatsTable);

                // 4. Map GrandPrixStats (Deeply nested table)
                season.OwnsOne(ss => ss.GrandPrixStats,
                    gp => { gp.ToTable(DbConstants.ConstructorConstants.GrandPrixStatisticsTable); });

                // 5. Map SprintStats (Deeply nested table)
                season.OwnsOne(ss => ss.SprintStats,
                    sprint => { sprint.ToTable(DbConstants.ConstructorConstants.SprintStatisticsTable); });
            });
        });
    }
}