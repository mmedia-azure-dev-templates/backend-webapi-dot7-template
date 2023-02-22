using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("Teams", Schema = "web")]
public class Team : Entity<TeamId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    [Required]
    public override TeamId Id { get; set; }
    public UserId UserId { get; set; }
    public HierarchyId HierarchyId { get; set; }
    public string DataKey { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
