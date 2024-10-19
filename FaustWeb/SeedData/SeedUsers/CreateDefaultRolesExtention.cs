using FaustWeb.Domain.DefaultIdentity;
using Microsoft.AspNetCore.Identity;

namespace FaustWeb.SeedData.SeedUsers;

public static class CreateDefaultRolesExtention
{
    public static async Task CreateDefaultRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[]
        {
            DefaultRoles.Admin,
            DefaultRoles.User,
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
