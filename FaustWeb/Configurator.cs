using FaustWeb.Application.Services.AuthService;
using FaustWeb.Application.Services.EmailService;
using FaustWeb.Domain.DTO.Email;
using FaustWeb.Infrastructure;
using FaustWeb.SeedData.SeedUsers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FaustWeb;

public static class Configurator
{
    public static void ConfigureDbConnection(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Connection not set");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connection));
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
    }

    public static async Task CreateDefaultIdentityAsync(this WebApplication app)
    {
        await app.CreateDefaultRolesAsync();
        await app.CreateDefaultUsersAsync();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(1));
    }

    public static void ConfigureAuth(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/authentication");
                options.AccessDeniedPath = new PathString("/authentication");
            });
    }

    public static void ConfigureEmail(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();

        builder.Services.AddSingleton(config!);
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
                    return [apiDesc.GroupName];
                }

                return [apiDesc.HttpMethod];
            });
        });
    }
}
