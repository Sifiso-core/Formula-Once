using FastEndpoints;
using FormulaOnce.Identity.Model;
using FormulaOnce.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace FormulaOnce.Identity.Endpoints.User.LoginUser;

public record LoginUserRequest(string Email, string Password);

public record LoginUserResponse(string Token, string Email);

public class LoginEndpoint(
    UserManager<ApplicationUser> userManager,
    JwtTokenService tokenService)
    : Endpoint<LoginUserRequest, LoginUserResponse>
{
    public override void Configure()
    {
        Post("/identity/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);

        if (user is null || !await userManager.CheckPasswordAsync(user, req.Password))
        {
            ThrowError("Invalid email or password");
        }

        var roles = await userManager.GetRolesAsync(user);

        var token = tokenService.GenerateToken(user, roles);

        await Send.OkAsync(new LoginUserResponse(token, user.Email!), cancellation: ct);
    }
}