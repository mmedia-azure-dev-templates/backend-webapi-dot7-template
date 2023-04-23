using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
public class PaymentMethodByIdResponse
{
    public PaymentMethodId Id { get; set; }
    public string? DataKey { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string? Display { get; set; }
    public bool Active { get; set; }
    public string? Icon { get; set; }
    public bool IsSelected { get; set; } = false;
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}