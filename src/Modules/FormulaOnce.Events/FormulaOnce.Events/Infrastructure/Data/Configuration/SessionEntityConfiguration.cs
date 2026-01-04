using FormulaOnce.Events.Domain.Race;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOnce.Events.Infrastructure.Data.Configuration;

public class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable(DbConstants.SessionConstants.TableName);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SessionType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.ScheduledStart).IsRequired();
    }
}