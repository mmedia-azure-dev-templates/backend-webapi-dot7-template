using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodUpdate;
public class PaymentMethodUpdateHandler : IRequestHandler<PaymentMethodUpdateRequest, PaymentMethodUpdateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public PaymentMethodUpdateHandler(IMapper mapper, IContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<PaymentMethodUpdateResponse> Handle(PaymentMethodUpdateRequest request, CancellationToken cancellationToken)
    {
        var paymentMethodUpdateResponse = new PaymentMethodUpdateResponse();
        var paymentMethod = await _context.PaymentMethods.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (paymentMethod != null)
        {
            paymentMethod.Icon = request.Icon;
            paymentMethod.Active = request.Active;
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync(cancellationToken);
            paymentMethodUpdateResponse.Success = true;
            paymentMethodUpdateResponse.Message = "Método de Pago Actualizado";
        }
        
        return paymentMethodUpdateResponse;
    }
}
