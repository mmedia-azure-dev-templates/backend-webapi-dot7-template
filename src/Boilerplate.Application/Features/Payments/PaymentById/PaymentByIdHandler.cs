using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Payments.PaymentById;
public class PaymentByIdHandler : IRequestHandler<PaymentByIdRequest, PaymentByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public PaymentByIdHandler(IContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaymentByIdResponse> Handle(PaymentByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await (from payment in _context.Payments.AsNoTracking().DefaultIfEmpty()
                            join paymentMethod in _context.PaymentMethods.AsNoTracking().DefaultIfEmpty() on payment.PaymentMethodId equals paymentMethod.Id
                            where payment.OrderId == request.OrderId
                            select new PaymentByIdResponse
                            {
                                OrderId = payment.OrderId,
                                PaymentMethodId = payment.PaymentMethodId,
                                Amount = payment.Amount,
                                Notes = payment.Notes,
                                Id = payment.Id,
                                DataKey = payment.DataKey,
                                PaymentMethodsType = paymentMethod.PaymentMethodsType,
                                Display = paymentMethod.Display,
                                Active = paymentMethod.Active,
                                Icon = paymentMethod.Icon,
                                DateCreated = payment.DateCreated,
                                DateUpdated = payment.DateUpdated
                            }).FirstOrDefaultAsync(cancellationToken);
        return result;
    }
}