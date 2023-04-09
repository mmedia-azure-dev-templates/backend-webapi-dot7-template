using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IOrderService
{
    public IQueryable<IOrderByIdResponse> GetLocalOrderById(OrderId orderId);
    public Task<IOrderByIdResponse> CheckValidOrderById(OrderId orderId, CancellationToken cancellationToken);
}
