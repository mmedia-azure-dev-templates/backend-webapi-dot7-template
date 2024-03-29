﻿using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }
    public DbSet<ApplicationUser> ApplicationUsers { get; }
    public DbSet<UserInformation> UserInformations { get; }
    public DbSet<GeographicLocation> GeographicLocations { get; set; }
    public DbSet<Team> Teams { get; }
    public DbSet<Article> Articles { get; }
    public DbSet<ArticleItem> ArticlesItems { get; }
    public DbSet<Counter> Counters { get; }
    public DbSet<OrderItem> OrderItems { get; }
    public DbSet<PaymentMethod> PaymentMethods { get; }
    public DbSet<Customer> Customers { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<Payment> Payments { get; }
    public DbSet<Address> Addresses { get; }
    public DbSet<Hero> Heroes { get; }
    public DbSet<CompanyTenant> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}