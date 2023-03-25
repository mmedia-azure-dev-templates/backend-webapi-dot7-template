using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Orders;

/* Class created because Nswag not working wih override parameters*/
public class PaymentMethodAllResponse
{
    public PaymentMethodId Id { get; set; }
    public string DataKey { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string Display { get; set; }
    public string? Icon { get; set; }
    public bool Active { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
