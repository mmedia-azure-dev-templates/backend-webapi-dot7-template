using Boilerplate.Api.Common;
using Boilerplate.Api.Configurations;
using Boilerplate.Domain.ClaimsChangeCode;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

// Controllers
builder.Services
    .AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
        options.Filters.Add<ValidationErrorResultFilter>();
    })
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddValidationSetup();

builder.Services.AddCacheSetup(builder.Environment);

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

app.UseSwaggerSetup();

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UsePermissionsChange();   //Example of updating the user's Permission claim when the database change in app using JWT Token for Authentication / Authorization
app.UseAddEmailClaimToUsers();//Example of adding an extra Email 

app.MapControllers()
   .RequireAuthorization();

await app.Migrate();

await app.RunAsync();