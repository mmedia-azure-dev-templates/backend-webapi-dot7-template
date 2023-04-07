using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Orders.OrderUpdate;
public class OrderUpdateResponse: IOrderUpdateResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false;
    public OrderNumber OrderNumber { get; set; }
    public OrderUpdateResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}
