using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodUpdate;
public class PaymentMethodUpdateRequest : IRequest<PaymentMethodUpdateResponse>
{
    public PaymentMethodId Id { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string Display { get; set; }
    public bool Active { get; set; }
    public string Icon { get; set; }
}
