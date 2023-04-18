using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodUpdate;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
public class PaymentMethodCreateHandler : IRequestHandler<PaymentMethodCreateRequest, PaymentMethodCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public PaymentMethodCreateHandler(IMapper mapper, IContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<PaymentMethodCreateResponse> Handle(PaymentMethodCreateRequest request, CancellationToken cancellationToken)
    {
        var paymentMethodCreateResponse = new PaymentMethodCreateResponse();
        PaymentMethod paymentMethodCreate = new()
        {
            PaymentMethodsType = request.PaymentMethodsType,
            Display = request.Display,
            Icon = request.Icon,
            Active = true,
        };

        _context.PaymentMethods.Add(paymentMethodCreate);
        await _context.SaveChangesAsync(cancellationToken);
        paymentMethodCreateResponse.Success = true;
        paymentMethodCreateResponse.Message = "Método de Pago Creado";
        return paymentMethodCreateResponse;
    }
}
