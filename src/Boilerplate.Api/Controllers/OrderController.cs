using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    [HttpGet]
    [Route("index")]
    public async Task<OrderItem> Index()
    {
        var orderItem = new OrderItem();
        return orderItem;
    }
}
