using FormulaOnce.Teams.Infrastructure;
using FormulaOnce.Teams.Infrastructure.Data;
using FormulaOnce.Teams.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace FormulaOnce.Teams;

public static class FormulaOnceTeamsExtensions
{
    public static IServiceCollection AddFormulaOnceTeams(this IServiceCollection services,
        ConfigurationManager configurationManager, ILogger logger)
    {
        services.AddDbContext<TeamsDbContext>(options =>
        {
            var connectionString = configurationManager.GetConnectionString("TeamsDatabase") ??
                                   throw new InvalidOperationException(
                                       "Connection string for the Teams module is not Configured.");
            options.UseSqlServer(configurationManager.GetConnectionString(connectionString));
        });

        services.AddScoped<IDriverRepository, DriverRepository>();

        services.AddScoped<IDriverService, DriverService>();

        logger.Information("::{module} module services registered", "Teams");

        return services;
    }
}