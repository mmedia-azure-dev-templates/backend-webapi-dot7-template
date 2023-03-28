using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Orders.OrderCreate;
public class OrderCreateResponse: IOrderCreateResponse
{
    public string Message { get; set; }
}
