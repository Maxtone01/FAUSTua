using FaustWeb.Domain.DefaultIdentity;
using Microsoft.AspNetCore.Identity;

namespace FaustWeb.SeedData.SeedUsers;

public static class CreateDefaultRolesExtension
{
    public static async Task CreateDefaultRolesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        var roles = new[]
        {
            DefaultRoles.Admin,
            DefaultRoles.User,
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
