using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("AspNetUsers", Schema = "dbo")]
public class ApplicationUser : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LegacyId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? LastLogin { get; set; }
    //public string DataKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}