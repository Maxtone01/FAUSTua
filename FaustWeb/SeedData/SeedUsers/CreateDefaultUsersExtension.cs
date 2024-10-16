using FaustWeb.Domain.DefaultIdentity;
using Microsoft.AspNetCore.Identity;

namespace FaustWeb.SeedData.SeedUsers;

public static class CreateDefaultUsersExtension
{
    public static async Task CreateDefaultUsersAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var adminUser = await userManager.FindByEmailAsync(DefaultUsers.AdminEmail);
        var defaultUser = await userManager.FindByEmailAsync(DefaultUsers.UserEmail);

        if (adminUser == null)
        {
            var admin = new IdentityUser
            {
                Email = DefaultUsers.AdminEmail,
                UserName = DefaultUsers.AdminName,
            };

            var adminResponse = await userManager.CreateAsync(admin, DefaultUsers.AdminPassword);

            if (!adminResponse.Succeeded)
            {
                var errors = string.Join(", ", adminResponse.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create admin: \n{errors}");
            }

            await userManager.AddToRoleAsync(admin, DefaultRoles.Admin);
        }

        if (defaultUser == null)
        {
            var user = new IdentityUser
            {
                Email = DefaultUsers.UserEmail,
                UserName = DefaultUsers.UserName,
            };

            var userResponse = await userManager.CreateAsync(user, DefaultUsers.UserPassword);
            if (!userResponse.Succeeded)
            {
                var errors = string.Join(", ", userResponse.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: \n{errors}");
            }

            await userManager.AddToRoleAsync(user, DefaultRoles.User);
        }
    }
}
