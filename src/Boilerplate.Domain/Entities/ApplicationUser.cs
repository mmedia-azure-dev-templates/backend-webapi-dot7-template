﻿using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("AspNetUsers", Schema = "dbo")]
public class ApplicationUser : IdentityUser<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LegacyId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? LastLogin { get; set; }
}