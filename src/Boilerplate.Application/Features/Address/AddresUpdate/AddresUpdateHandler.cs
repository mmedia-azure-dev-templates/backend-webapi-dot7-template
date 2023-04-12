using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Address.AddresCreate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using org.apache.zookeeper.data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Address.AddresUpdate;
public class AddresUpdateHandler : IRequestHandler<AddresUpdateRequest, AddresUpdateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private AddresUpdateResponse _addresUpdateResponse;

    public AddresUpdateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<AddresUpdateResponse> Handle(AddresUpdateRequest request, CancellationToken cancellationToken)
    {
        var address = await _context.Address.Where(x=> x.PersonId == request.PersonId).FirstOrDefaultAsync(cancellationToken);
        if (address != null) 
        {
            _context.Address.Update(address);
            await _context.SaveChangesAsync(cancellationToken);
            _addresUpdateResponse = _mapper.Map(address, _addresUpdateResponse);
            _addresUpdateResponse.PersonId = request.PersonId;
        }
        return _addresUpdateResponse;
    }
}
