using Boilerplate.Application.Common;
using System;
using System.Collections.Generic;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Api.IntegrationTests.Helpers;

public static class TestingDatabase
{
    public static async Task SeedDatabase(Func<IContext> contextFactory)
    {
        //await using var db = contextFactory();
        //await db.Users.ExecuteDeleteAsync();
        //db.Heroes.AddRange(GetSeedingHeroes);
        //db.Users.AddRange(GetSeedingUsers);
        //await db.SaveChangesAsync();
    }

    //public static readonly User[] GetSeedingUsers = new[]
    //{
    //    new User()
    //    {
    //        Id = new Guid(),
    //        Password = BC.HashPassword("testpassword123"),
    //        Email = "admin@boilerplate.com",
    //        Role = "Admin"
    //    },
    //    new User()
    //    {
    //        Id = new Guid(),
    //        Password = BC.HashPassword("testpassword123"),
    //        Email = "user@boilerplate.com",
    //        Role = "User"
    //    }
    //};

    public static readonly List<Hero> GetSeedingHeroes =
        new()
        {
            new(){ Id = 1, Name = "Corban Best", HeroType = HeroType.ProHero },
            new() { Id = 2, Name = "Priya Hull", HeroType = HeroType.Student },
            new() { Id = 3, Name = "Harrison Vu", HeroType = HeroType.Teacher }
        };
}