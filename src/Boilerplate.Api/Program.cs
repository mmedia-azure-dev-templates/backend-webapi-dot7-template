using Boilerplate.Api.Common;
using Boilerplate.Api.Configurations;
using Boilerplate.Application.Services;
using Boilerplate.Domain.ClaimsChangeCode;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Persistence
builder.Services.AddPersistenceSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

// Request response compression
builder.Services.AddCompressionSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatRSetup();

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

// Add jwt
builder.Services.AddJwtSetup(builder.Configuration);

//Render Email Templates
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
//builder.Services.AddTransient<RazorViewToStringRenderer>();

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

// Add Localization
builder.Services.AddLocalizationSetup();

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
//app.UseSwaggerAuthorizedMiddleware();
app.UseSwaggerSetup();
await app.Migrate();
await app.RunAsync();