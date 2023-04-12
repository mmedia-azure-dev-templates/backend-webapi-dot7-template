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
public class AddressUpdateHandler : IRequestHandler<AddressUpdateRequest, AddressUpdateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private AddressUpdateResponse _addresUpdateResponse;

    public AddressUpdateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<AddressUpdateResponse> Handle(AddressUpdateRequest request, CancellationToken cancellationToken)
    {
        var address = await _context.Addresses.Where(x=> x.PersonId == request.PersonId).FirstOrDefaultAsync(cancellationToken);
        if (address != null) 
        {
            address.PersonId = request.PersonId;
            address.PrimaryStreet = request.PrimaryStreet;
            address.SecondaryStreet = request.SecondaryStreet;
            address.Numeration = request.Numeration;
            address.Reference = request.Reference;
            address.Provincia = request.Provincia;
            address.Canton = request.Canton;
            address.Parroquia = request.Parroquia;
            address.Notes = request.Notes;
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync(cancellationToken);
            _addresUpdateResponse = _mapper.Map(address, _addresUpdateResponse);
            _addresUpdateResponse.PersonId = request.PersonId;
        }
        return _addresUpdateResponse;
    }
}
