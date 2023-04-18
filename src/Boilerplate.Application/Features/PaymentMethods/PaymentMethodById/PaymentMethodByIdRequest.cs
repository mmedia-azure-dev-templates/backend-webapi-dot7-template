using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
public class PaymentMethodByIdRequest : IRequest<PaymentMethodByIdResponse>
{
    public PaymentMethodId Id { get; set; }
}
