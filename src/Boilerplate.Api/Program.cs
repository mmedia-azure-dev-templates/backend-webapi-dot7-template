using Boilerplate.Api.Common;
using Boilerplate.Api.Configurations;
using Boilerplate.Application;
using Boilerplate.Application.Auth;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Auth.ForgotPassword;
using Boilerplate.Application.Features.Auth.ResetPassword;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.OrderItems.OrderItemCreate;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Orders.OrderCreate;
using Boilerplate.Application.Features.Orders.OrderUpdate;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.EditUser;
using Boilerplate.Application.Services;
using Boilerplate.Domain.ClaimsChangeCode;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Boilerplate.Infrastructure;
using Boilerplate.Infrastructure.Context;
using MassTransit.NewIdProviders;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json.Serialization;
using Boilerplate.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISession, Session>();

builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureDomainServices(builder.Configuration);
builder.Services.AddScoped<IContext, ApplicationDbContext>();
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

//Render Email Templates
builder.Services.AddRazorPages();
builder.Services.AddScoped<SweetAlert, SweetAlert>();
builder.Services.AddScoped<IForgotPasswordResponse, ForgotPasswordResponse>();
builder.Services.AddScoped<IAuthenticateResponse, AuthenticateResponse>();
builder.Services.AddScoped<IResetPasswordResponse, ResetPasswordResponse>();
builder.Services.AddScoped<IUserResponse, UserResponse>();
builder.Services.AddScoped<IEditUserResponse, EditUserResponse>();
builder.Services.AddScoped<IArticleCreateResponse, ArticleCreateResponse>();
builder.Services.AddScoped<IOrderCreateResponse, OrderCreateResponse>();
builder.Services.AddScoped<IOrderUpdateResponse, OrderUpdateResponse>();
builder.Services.AddScoped<IOrderItemCreateResponse, OrderItemCreateResponse>();
builder.Services.AddScoped<IPaymentMethodCreateResponse, PaymentMethodCreateResponse>();
builder.Services.AddScoped<ICustomerCreateResponse, CustomerCreateResponse>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderByIdResponse, OrderByIdResponse>();
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
if (app.Environment.IsProduction())
{
    app.UseSwaggerAuthorizedMiddleware();
}
app.UseSwaggerSetup();
await app.Migrate();
await app.RunAsync();