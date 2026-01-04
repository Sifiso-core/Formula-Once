using FormulaOnce.Events.Infrastructure.Data;
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
        
        logger.Information("::{module} module services registered", "Events");
        return services;
    }
}