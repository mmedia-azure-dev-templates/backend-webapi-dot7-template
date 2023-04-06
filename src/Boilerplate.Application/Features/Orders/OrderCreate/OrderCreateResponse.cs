using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Orders.OrderCreate;
public class OrderCreateResponse: IOrderCreateResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false;
    public OrderNumber OrderNumber { get; set; }
    public OrderCreateResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}
