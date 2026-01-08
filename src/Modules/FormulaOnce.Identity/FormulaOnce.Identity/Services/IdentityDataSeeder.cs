using FormulaOnce.Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace FormulaOnce.Identity.Services;

public static class IdentityDataSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roleNames = ["Admin", "Fan"];
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }

        const string adminEmail = "admin@formulaonce.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true
            };

            var createAdmin = await userManager.CreateAsync(newAdmin, "Formula1Secret2026!");

            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}