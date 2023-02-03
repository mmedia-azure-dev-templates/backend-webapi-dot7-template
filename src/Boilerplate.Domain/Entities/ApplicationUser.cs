using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("AspNetUsers", Schema = "dbo")]
public class ApplicationUser : IdentityUser
{
    public int LegacyId { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? LastLogin { get; set; }
}