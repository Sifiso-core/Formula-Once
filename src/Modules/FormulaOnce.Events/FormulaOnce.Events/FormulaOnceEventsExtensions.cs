using FormulaOnce.Events.Domain.Services;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Infrastructure.Data;
using FormulaOnce.Events.Infrastructure.Repositories.CircuitRepository;
using FormulaOnce.Events.Infrastructure.Repositories.RaceRepository;
using FormulaOnce.Events.Services.CircuitService;
using FormulaOnce.Events.Services.RaceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;

namespace FormulaOnce.Events;

public static class FormulaOnceEventsExtensions
{
    public static IServiceCollection AddFormulaOnceEvents(this IServiceCollection services,
        ConfigurationManager configurationManager, ILogger logger)
    {
        services.AddDbContext<EventsDbContext>(options =>
        {
            var connectionString = configurationManager.GetConnectionString("EventsDatabase") ??
                                   throw new InvalidOperationException(
                                       "Connection string for the Events module is not Configured.");

            options.UseSqlServer(connectionString,
                sqlOptions =>
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, DbConstants.Schema));
        });

        services.AddScoped<IRaceRepository, RaceRepository>();

        services.AddScoped<ICircuitRepository, CircuitRepository>();

        services.AddScoped<RaceWeekendScheduler>();

        services.AddScoped<ICircuitService, CircuitService>();
        
        services.AddScoped<IRaceService, RaceService>();

        logger.Information("::{module} module services registered", "Events");
        return services;
    }
}