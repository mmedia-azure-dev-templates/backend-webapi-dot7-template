using Boilerplate.Domain.Entities.Common;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodPriorityUpdate;
public class PaymentMethodPriorityUpdateRequest : IRequest<PaymentMethodPriorityUpdateResponse>
{
    public required List<PaymentMethodPriorityUpdateRequestDto> PaymentMethodPriorityUpdateRequestDto { get; set; }
}


public class PaymentMethodPriorityUpdateRequestDto
{
    public PaymentMethodId Id { get; set; }
    public int Priority { get; set; }
}