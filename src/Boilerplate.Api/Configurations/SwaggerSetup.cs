using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Boilerplate.Api.Configurations;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1", 
                new OpenApiInfo 
                { 
                    Title = "Jiban", 
                    Version = "v1" 
                }
            );

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

            c.AddSecurityRequirement(securityRequirement);

            // Maps all structured ids to the guid type to show correctly on swagger
            var allGuids = typeof(IGuid).Assembly.GetTypes().Where(type => typeof(IGuid).IsAssignableFrom(type) && !type.IsInterface).ToList();
            foreach (var guid in allGuids)
            {
                c.MapType(guid, () => new OpenApiSchema { Type = "string", Format = "uuid" });
            }

            var allLongs = typeof(ILong).Assembly.GetTypes().Where(type => typeof(ILong).IsAssignableFrom(type) && !type.IsInterface).ToList();
            foreach (var allLong in allLongs)
            {
                c.MapType(allLong, () => new OpenApiSchema { Type = "integer", Format = "int64" });
            }

            var allInts = typeof(IInt).Assembly.GetTypes().Where(type => typeof(IInt).IsAssignableFrom(type) && !type.IsInterface).ToList();
            foreach (var allInt in allInts)
            {
                c.MapType(allInt, () => new OpenApiSchema { Type = "integer", Format = "int32" });
            }

            var allStrings = typeof(IString).Assembly.GetTypes().Where(type => typeof(IString).IsAssignableFrom(type) && !type.IsInterface).ToList();
            foreach (var allString in allStrings)
            {
                c.MapType(allString, () => new OpenApiSchema { Type = "string" });
            }
        });


        services.AddControllersWithViews().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        return services;
    }

    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jiban v1");
                c.DocExpansion(DocExpansion.List);
                c.DisplayRequestDuration();
            });
        return app;
    }
}