using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ReservationApi.Settings
{
    public class ConfigureAuthSwagger : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica se o endpoint tem [Authorize]
            var declaringType = context.MethodInfo.DeclaringType;

            bool hasAuthorize =
                (declaringType?.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>()
                    .Any() ?? false)
                ||
                context.MethodInfo.GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>()
                    .Any();

            if (hasAuthorize) //Se o endpoint tem [Authorize] add no swagger o cadeado
            {
                operation.Security =
            [
                new() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            ];
            }
        }
    }
}
