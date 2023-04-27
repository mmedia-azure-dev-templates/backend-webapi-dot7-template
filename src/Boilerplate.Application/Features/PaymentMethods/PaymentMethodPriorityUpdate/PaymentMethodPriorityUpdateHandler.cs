using Boilerplate.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodPriorityUpdate;
public class PaymentMethodPriorityUpdateHandler : IRequestHandler<PaymentMethodPriorityUpdateRequest, PaymentMethodPriorityUpdateResponse>
{
    private readonly IContext _context;

    public PaymentMethodPriorityUpdateHandler(IContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
    }
    public async Task<PaymentMethodPriorityUpdateResponse> Handle(PaymentMethodPriorityUpdateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var paymentMethodPriorityUpdateResponse = new PaymentMethodPriorityUpdateResponse();
            var paymentMethods = await _context.PaymentMethods.Where(x => request.PaymentMethodPriorityUpdateRequestDto.Select(y => y.Id).Contains(x.Id)).ToListAsync(cancellationToken);

            foreach(var paymentMethod in paymentMethods)
            {
                var paymentPriority = request.PaymentMethodPriorityUpdateRequestDto.Where(x => x.Id == paymentMethod.Id).FirstOrDefault();
                paymentMethod.Priority = paymentPriority!.Priority;
                _context.PaymentMethods.Update(paymentMethod);
            }

            await _context.SaveChangesAsync(cancellationToken);
            scope.Complete();
            paymentMethodPriorityUpdateResponse.Success = true;
            paymentMethodPriorityUpdateResponse.Message = "Prioridad de pago no actualizado";
            return paymentMethodPriorityUpdateResponse;
        }
    }
}
