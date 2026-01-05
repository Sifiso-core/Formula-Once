using System.Security.Claims;
using FastEndpoints.Security;
using FormulaOnce.Identity.Model;
using FormulaOnce.Identity.Options;
using Microsoft.Extensions.Options;

namespace FormulaOnce.Identity.Services;

public class JwtTokenService(IOptions<JwtOptions> jwtOptions)
{
    private readonly JwtOptions _options = jwtOptions.Value;

    public string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        return JwtBearer.CreateToken(options =>
        {
            options.SigningKey = _options.Key;
            options.ExpireAt = DateTime.UtcNow.AddDays(_options.ExpiryInDays);
            options.Issuer = _options.Issuer;
            options.Audience = _options.Audience;


            options.User.Roles.AddRange(roles);

            options.User.Claims.Add((ClaimTypes.NameIdentifier, user.Id.ToString()));
            options.User.Claims.Add((ClaimTypes.Email, user.Email!));
            options.User.Claims.Add(("FirstName", user.FirstName));
            options.User.Claims.Add(("LastName", user.LastName));
        });
    }
}