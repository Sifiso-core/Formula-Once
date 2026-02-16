using FastEndpoints;
using FormulaOnce.Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace FormulaOnce.Identity.Endpoints.User.RegisterUser;

public record RegisterRequest(string Email, string Password, string FirstName, string LastName);

public class RegisterEndpoint(UserManager<ApplicationUser> userManager)
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("/identity/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var user = new ApplicationUser
        {
            UserName = req.Email,
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName
        };

        var result = await userManager.CreateAsync(user, req.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                AddError(error.Description);

            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        await Send.OkAsync(cancellation: ct);
    }
}