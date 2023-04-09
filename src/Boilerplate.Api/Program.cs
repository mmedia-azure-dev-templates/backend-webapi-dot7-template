using Boilerplate.Api.Common;
using Boilerplate.Api.Configurations;
using Boilerplate.Application;
using Boilerplate.Domain;
using Boilerplate.Domain.ClaimsChangeCode;
using Boilerplate.Infrastructure;
using Boilerplate.Infrastructure.Configuration;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureDomainServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

NewId.SetProcessIdProvider(new CurrentProcessIdProvider());

// Request response compression
builder.Services.AddCompressionSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

// Middleware
builder.Services.AddScoped<ExceptionHandlerMiddleware>();

builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);
}

// Add opentelemetry
builder.AddOpenTemeletrySetup();

// Swagger
builder.Services.AddSwaggerSetup();

builder.Services.AddRazorPages();

// Controllers
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Add Cache Setup
builder.Services.AddCacheSetup(builder.Environment);

// Add Email Setup
builder.Services.AddMailSetup(builder.Configuration);
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromHours(4));


var app = builder.Build();

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );
// Configure the HTTP request pipeline.
app.UseResponseCompression();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseAuthorization();
app.UsePermissionsChange();   //Example of updating the user's Permission claim when the database change in app using JWT Token for Authentication / Authorization
app.UseAddEmailClaimToUsers();//Example of adding an extra Email 
app.MapControllers().RequireAuthorization();
app.UseLocalizationSetup();
if (app.Environment.IsProduction())
{
    app.UseSwaggerAuthorizedMiddleware();
}
app.UseSwaggerSetup();
await app.Migrate();
await app.RunAsync();