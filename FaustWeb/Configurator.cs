using FaustWeb.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FaustWeb;

public static class Configurator
{
    public static void ConfigureDbConnection(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        if (connection == null)
            throw new Exception("Connection not set");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connection));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Faust API",
                Description = "Faust API testing"
            });

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var groupName = apiDesc.GroupName ?? string.Empty;
                var isInGroup = docName == groupName || docName == "v1";

                var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"];
                var isLibraryController = controllerName == "Api";

                return isInGroup && isLibraryController;
            });

            c.TagActionsBy(apiDesc =>
            {
                if (apiDesc.GroupName != null)
                {
                    return new[] { apiDesc.GroupName };
                }

                return new[] { apiDesc.HttpMethod };
            });
        });
    }
}
