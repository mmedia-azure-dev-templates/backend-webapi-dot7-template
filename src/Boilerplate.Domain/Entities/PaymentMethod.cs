using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Domain.Entities;
public class PaymentMethod : Entity<PaymentMethodId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override PaymentMethodId Id { get; set; }
    public string DataKey { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}