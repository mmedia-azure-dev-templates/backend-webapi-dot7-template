using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
public class PaymentMethodCreateHandler : IRequestHandler<PaymentMethodByIdRequest, PaymentMethodByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public PaymentMethodCreateHandler(IMapper mapper, IContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<PaymentMethodByIdResponse> Handle(PaymentMethodByIdRequest request, CancellationToken cancellationToken)
    {
        var paymentMethodByIdResponse = new PaymentMethodByIdResponse();
        var paymentMethod = await _context.PaymentMethods.Where(x=> x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if(paymentMethod != null)
        {
            paymentMethodByIdResponse.Id = paymentMethod.Id;
            paymentMethodByIdResponse.PaymentMethodsType = paymentMethod.PaymentMethodsType;
            paymentMethodByIdResponse.Display = paymentMethod.Display;
            paymentMethodByIdResponse.DataKey = paymentMethod.DataKey;
            paymentMethodByIdResponse.Icon = paymentMethod.Icon;
            paymentMethodByIdResponse.Active = paymentMethod.Active;
            paymentMethodByIdResponse.DateCreated = paymentMethod.DateCreated;
            paymentMethodByIdResponse.DateUpdated = paymentMethod.DateUpdated;
        }
        
        return paymentMethodByIdResponse;
    }
}
