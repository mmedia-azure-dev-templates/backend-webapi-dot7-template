﻿using Boilerplate.Application.Common.Behaviors;
using Boilerplate.Application.Common.Handlers;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Boilerplate.Application.MappingProfiles;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Application.Features.Auth.ForgotPassword;
using Boilerplate.Application.Features.Auth.ResetPassword;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.OrderItems.OrderItemCreate;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Orders.OrderCreate;
using Boilerplate.Application.Features.Orders.OrderUpdate;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
using Boilerplate.Application.Features.Users.EditUser;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Services;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Boilerplate.Application.Auth;
using Boilerplate.Application.Features.Pdfs;

namespace Boilerplate.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Boilerplate.Application.IAssemblyMarker).Assembly));
        //services.AddMediatR(typeof(Boilerplate.Application.IAssemblyMarker).GetTypeInfo().Assembly);
        services.AddScoped<INotificationHandler<ValidationError>, ValidationErrorHandler>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<SweetAlert, SweetAlert>();
        services.AddScoped<ISession, Session>();
        services.AddScoped<IForgotPasswordResponse, ForgotPasswordResponse>();
        services.AddScoped<IAuthenticateResponse, AuthenticateResponse>();
        services.AddScoped<IResetPasswordResponse, ResetPasswordResponse>();
        services.AddScoped<IUserResponse, UserResponse>();
        services.AddScoped<IEditUserResponse, EditUserResponse>();
        services.AddScoped<IArticleCreateResponse, ArticleCreateResponse>();
        services.AddScoped<IOrderCreateResponse, OrderCreateResponse>();
        services.AddScoped<IOrderUpdateResponse, OrderUpdateResponse>();
        services.AddScoped<IOrderItemCreateResponse, OrderItemCreateResponse>();
        services.AddScoped<IPaymentMethodCreateResponse, PaymentMethodCreateResponse>();
        services.AddScoped<ICustomerCreateResponse, CustomerCreateResponse>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IOrderByIdResponse, OrderByIdResponse>();
        return services;
    }
}
