using System.Reflection;
using FormulaOnce.Teams.Domain;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Teams.Infrastructure.Data;

internal class TeamsDbContext : DbContext
{
    public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
    {
        
    }

    public DbSet<Driver> Drivers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.TeamSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public static class DbConstants
{
    public const string TeamSchema = "Teams";
}