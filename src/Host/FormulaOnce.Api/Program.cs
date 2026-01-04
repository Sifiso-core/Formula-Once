using FastEndpoints;
using FormulaOnce.Events;
using FormulaOnce.Teams;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();

builder.Services.AddFormulaOnceTeams(builder.Configuration, logger);

builder.Services.AddFormulaOnceEvents(builder.Configuration,logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseFastEndpoints(options => { options.Errors.UseProblemDetails(); });

app.Run();