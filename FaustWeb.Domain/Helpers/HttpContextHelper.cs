using Microsoft.AspNetCore.Http;

namespace FaustWeb.Domain.Helpers;

public class HttpContextHelper
{
    public static string GetClientUri(HttpContext httpContext)
    {
        if (httpContext?.Request != null)
        {
            var request = httpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }

        return string.Empty;
    }
}
