using System.Text;
using FastEndpoints.Security;
using FormulaOnce.Identity.Data;
using FormulaOnce.Identity.Model;
using FormulaOnce.Identity.Options;
using FormulaOnce.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FormulaOnce.Identity;

public static class FormulaOnceIdentityExtensions
{
    public static IServiceCollection AddFormulaOnceIdentity(this IServiceCollection services,
        ConfigurationManager configuration, ILogger logger)
    {
        var connectionString = configuration.GetConnectionString("IdentityDatabase") ??
                               throw new InvalidOperationException(
                                   "Connection string for the Identity module is not Configured.");

        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(options => !string.IsNullOrEmpty(options.Key), "JWT Key is missing in configuration!")
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthenticationJwtBearer(options =>
        {
            options.SigningKey = jwtSettings!.Key;
            options.SigningStyle = TokenSigningStyle.Symmetric;
        }, bearerOptions =>
        {
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings!.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                RoleClaimType = "role",
            };
        });

        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization(options => { options.AddPolicy("AdminOnly", b => b.RequireRole("Admin")); });

        services.AddScoped<JwtTokenService>();

        logger.Information("::{module} module services registered", "Identity");

        return services;
    }
}