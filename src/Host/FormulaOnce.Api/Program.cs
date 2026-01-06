using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using FastEndpoints;
using FormulaOnce.Api.Middleware;
using FormulaOnce.Commerce;
using FormulaOnce.Events;
using FormulaOnce.Identity;
using FormulaOnce.Identity.Model;
using FormulaOnce.Identity.Services;
using FormulaOnce.Teams;
using Microsoft.AspNetCore.Identity;
using Serilog;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var logger = Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddFastEndpoints();

builder.Services.AddFormulaOnceIdentity(builder.Configuration, logger);

builder.Services.AddFormulaOnceTeams(builder.Configuration, logger);

builder.Services.AddFormulaOnceEvents(builder.Configuration, logger);

builder.Services.AddFormulaOnceCommerce(builder.Configuration, logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await IdentityDataSeeder.SeedAsync(userManager, roleManager);
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.UseFastEndpoints(options =>
{
    options.Errors.UseProblemDetails();
    options.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
});

app.Run();