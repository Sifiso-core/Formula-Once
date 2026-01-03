using System.Reflection;
using FormulaOnce.Teams.Domain.Constructor;
using FormulaOnce.Teams.Domain.Driver;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Teams.Infrastructure.Data;

internal class TeamsDbContext : DbContext
{
    public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Constructor> Constructors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.TeamSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}