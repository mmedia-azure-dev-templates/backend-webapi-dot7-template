using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.Graph.ExternalConnectors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Counters", Schema = "web")]
public class Counter : Entity<CounterId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override CounterId Id { get; set; }
    public string DataKey { get; set; }
    public string Slug { get; set; } = null!;
    public CustomCounter CustomCounter { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
