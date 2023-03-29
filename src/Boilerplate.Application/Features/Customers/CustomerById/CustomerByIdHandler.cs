using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Boilerplate.Application.Features.Customers.CustomerById;

public class CustomerByIdHandler : IRequestHandler<CustomerByIdRequest, CustomerByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public CustomerByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CustomerByIdResponse> Handle(CustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.Where(x => x.Id == request.CustomerId).FirstOrDefaultAsync(cancellationToken);
        
        return _mapper.Map<CustomerByIdResponse>(customer);
    }
}