using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using Microsoft.Graph.ExternalConnectors;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("PaymentMethods", Schema = "web")]
public class PaymentMethod : Entity<PaymentMethodId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override PaymentMethodId Id { get; set; }
    public string DataKey { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string Display { get; set; }
    public bool Active { get; set; }
    public string? Icon { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}