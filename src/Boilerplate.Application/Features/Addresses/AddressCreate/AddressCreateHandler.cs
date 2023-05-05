using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Addresses.AddressCreate;
public class AddressCreateHandler : IRequestHandler<AddressCreateRequest, AddressCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private AddressCreateResponse _addresCreateResponse;

    public AddressCreateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<AddressCreateResponse> Handle(AddressCreateRequest request, CancellationToken cancellationToken)
    {
        var address = new Address
        {
            Id = new AddressId(Guid.NewGuid()),
            PersonId = new PersonId((Guid)request.PersonId),
            PrimaryStreet = request.PrimaryStreet,
            SecondaryStreet = request.SecondaryStreet,
            Numeration = request.Numeration,
            Reference = request.Reference,
            Provincia = request.Provincia,
            Canton = request.Canton,
            Parroquia = request.Parroquia,
        };
        await _context.Addresses.AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _addresCreateResponse = _mapper.Map(address, _addresCreateResponse);
        _addresCreateResponse.PersonId = address.PersonId;
        if (
            address.DataKey != null &&
            address.PrimaryStreet != null &&
            address.SecondaryStreet != null &&
            address.Numeration != null &&
            address.Reference != null &&
            address.Provincia != null &&
            address.Canton != null &&
            address.Parroquia != null)
        {
            _addresCreateResponse.AddressComplete = true;
        }
        return _addresCreateResponse;
    }
}
