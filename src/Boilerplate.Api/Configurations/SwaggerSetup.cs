using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using System.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Boilerplate.Api.Configurations;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddOpenApiDocument(config =>
        {
            config.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
            };
            config.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.Http,
                In = OpenApiSecurityApiKeyLocation.Header,
                Scheme = "bearer",
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            });

            config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            config.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "Jiban Platform";
                document.Info.Description = "Jiban Platform Web API";
            };
        });
  

      

        //    c.AddSecurityDefinition("Bearer", securitySchema);

        //    var securityRequirement = new OpenApiSecurityRequirement
        //        {
        //            { securitySchema, new[] { "Bearer" } }
        //        };

        //    c.AddSecurityRequirement(securityRequirement);

        //    c.DescribeAllParametersInCamelCase();

        //    // Maps all structured ids to the guid type to show correctly on swagger
        //    var allGuids = typeof(IGuid).Assembly.GetTypes().Where(type => typeof(IGuid).IsAssignableFrom(type) && !type.IsInterface).ToList();
        //    foreach (var guid in allGuids)
        //    {
        //        c.MapType(guid, () => new OpenApiSchema { Type = "string", Format = "uuid" });
        //    }

        //    var allLongs = typeof(ILong).Assembly.GetTypes().Where(type => typeof(ILong).IsAssignableFrom(type) && !type.IsInterface).ToList();
        //    foreach (var allLong in allLongs)
        //    {
        //        c.MapType(allLong, () => new OpenApiSchema { Type = "integer", Format = "int64" });
        //    }

        //    var allInts = typeof(IInt).Assembly.GetTypes().Where(type => typeof(IInt).IsAssignableFrom(type) && !type.IsInterface).ToList();
        //    foreach (var allInt in allInts)
        //    {
        //        c.MapType(allInt, () => new OpenApiSchema { Type = "integer", Format = "int32" });
        //    }

        //    var allStrings = typeof(IString).Assembly.GetTypes().Where(type => typeof(IString).IsAssignableFrom(type) && !type.IsInterface).ToList();
        //    foreach (var allString in allStrings)
        //    {
        //        c.MapType(allString, () => new OpenApiSchema { Type = "string" });
        //    }
        //});

        return services;
    }

    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
    {
        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
            settings.DocumentTitle = "Jiban Api Platform";
            settings.PersistAuthorization = true;
            settings.TransformToExternalPath = (internalUiRoute, request) =>
            {
                if (internalUiRoute.StartsWith("/") == true && internalUiRoute.StartsWith(request.PathBase) == false)
                {
                    return request.PathBase + internalUiRoute;
                }
                else
                {
                    return internalUiRoute;
                }
            };
        });
        return app;
    }
}