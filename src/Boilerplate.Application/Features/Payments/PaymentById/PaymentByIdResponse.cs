using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Payments.PaymentById;
public class PaymentByIdResponse
{
    public PaymentId Id { get; set; }
    public string DataKey { get; set; }
    public OrderId OrderId { get; set; }
    public PaymentMethodId? PaymentMethodId { get; set; }
    public decimal? Amount { get; set; }
    public string? Notes { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string Display { get; set; }
    public bool Active { get; set; }
    public string? Icon { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}