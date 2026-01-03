using FormulaOnce.Teams.Infrastructure.ConstructorRepository;
using FormulaOnce.Teams.Infrastructure.Data;
using FormulaOnce.Teams.Infrastructure.DriverRepository;
using FormulaOnce.Teams.Services.ConstructorServices;
using FormulaOnce.Teams.Services.DriverServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            options.UseSqlServer(connectionString,
                sqlserverOptions =>
                    sqlserverOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                        DbConstants.TeamSchema));
        });

        services.AddScoped<IDriverRepository, DriverRepository>();

        services.AddScoped<IDriverService, DriverService>();

        services.AddScoped<IConstructorRepository, ConstructorRepository>();

        services.AddScoped<IConstructorService, ConstructorService>();

        logger.Information("::{module} module services registered", "Teams");

        return services;
    }
}