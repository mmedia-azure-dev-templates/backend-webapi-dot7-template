using Microsoft.AspNetCore.Identity;
using System;

namespace Boilerplate.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? LastLogin { get; set; }
}