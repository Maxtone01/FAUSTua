using Microsoft.OpenApi.Models;

namespace FaustWeb;

public static class Configurator
{
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
