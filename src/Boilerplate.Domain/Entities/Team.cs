using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boilerplate.Domain.Entities;
public class Team : Entity<TeamId>, IDateCreatedAndUpdated
{
    public TeamId Id { get; set; }
    public UserId UserId { get; set; }
    public HierarchyId HierarchyId { get; set; }
    public HierarchyId OldHyerarchyId { get; set; }
    public string DataKey { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
