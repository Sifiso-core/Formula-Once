using System.Reflection;
using FormulaOnce.Events.Domain.Circuit;
using FormulaOnce.Events.Domain.Race;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Events.Infrastructure.Data;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<Race> Races { get; set; }

    public DbSet<Circuit> Circuits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}