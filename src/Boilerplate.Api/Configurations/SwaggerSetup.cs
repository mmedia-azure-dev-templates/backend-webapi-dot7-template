﻿using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Boilerplate.Api.Configurations;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "Example2.WebApiWithToken.IndividualAccounts", 
                Version = "v1",
                Description = "API Boilerplate",
                Contact = new OpenApiContact
                {
                    Name = "Yan Pitangui",
                    Url = new Uri("https://github.com/yanpitangui")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://github.com/yanpitangui/dotnet-api-boilerplate/blob/main/LICENSE")
                }
            });

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

            c.DescribeAllParametersInCamelCase();
            c.OrderActionsBy(x => x.RelativePath);

            var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();

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
        /*
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Boilerplate.Api",
                    Version = "v1",
                    Description = "API Boilerplate",
                    Contact = new OpenApiContact
                    {
                        Name = "Yan Pitangui",
                        Url = new Uri("https://github.com/yanpitangui")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/yanpitangui/dotnet-api-boilerplate/blob/main/LICENSE")
                    }
                });
            c.DescribeAllParametersInCamelCase();
            c.OrderActionsBy(x => x.RelativePath);

            var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            // To Enable authorization using Swagger (JWT)    
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });

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

        });*/
        return services;
    }

    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example2.WebApiWithToken.IndividualAccounts v1");
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocExpansion(DocExpansion.List);
                c.DisplayRequestDuration();
            });
        return app;
    }
}