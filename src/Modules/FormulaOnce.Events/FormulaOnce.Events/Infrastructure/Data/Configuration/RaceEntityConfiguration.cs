using FormulaOnce.Events.Domain.Circuit;
using FormulaOnce.Events.Domain.Race;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Events.Infrastructure.Data.Configuration;

public class RaceEntityConfiguration : IEntityTypeConfiguration<Race>
{
    public void Configure(EntityTypeBuilder<Race> builder)
    {
        builder.ToTable(DbConstants.RaceConstants.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Season).IsRequired();
        builder.Property(x => x.Round).IsRequired();

        // Relationship to Circuit (Foreign Key)
        builder.HasOne<Circuit>()
            .WithMany()
            .HasForeignKey(x => x.CircuitId)
            .OnDelete(DeleteBehavior.Restrict); // Don't delete circuit if race is deleted

        // Relationship to Sessions
        builder.HasMany(x => x.Sessions)
            .WithOne()
            .HasForeignKey("RaceId")
            .OnDelete(DeleteBehavior.Cascade); // If race is cancelled/deleted, delete sessions
    }
}