using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Customers.CustomerById;

public class CustomerByIdRequest:IRequest<CustomerByIdResponse> {
    public CustomerId CustomerId { get; init; }
}