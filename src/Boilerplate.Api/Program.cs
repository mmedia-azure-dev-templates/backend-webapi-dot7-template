using Boilerplate.Api.Common;
using Boilerplate.Api.Configurations;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);



// Authn / Authrz
//builder.Services.AddAuthSetup(builder.Configuration);

// Swagger
//builder.Services.AddSwaggerSetup();

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

// Add jwt
builder.Services.AddJwtSetup(builder.Configuration);


// Controllers
builder.Services
    .AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
        options.Filters.Add<ValidationErrorResultFilter>();
    })
    .AddValidationSetup();

builder.Services.AddCacheSetup(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseSwaggerSetup();

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
   .RequireAuthorization();

await app.Migrate();

await app.RunAsync();