using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Address.AddresCreate;
public class AddresCreateHandler : IRequestHandler<AddresCreateRequest, AddresCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private AddresCreateResponse _addresCreateResponse;

    public AddresCreateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<AddresCreateResponse> Handle(AddresCreateRequest request, CancellationToken cancellationToken)
    {
        var address = new Addres
        {
            Id = new AddresId(Guid.NewGuid()),
            PersonId = new PersonId((Guid)request.PersonId),
            PrimaryStreet = request.PrimaryStreet,
            SecondaryStreet = request.SecondaryStreet,
            Numeration = request.Numeration,
            Reference = request.Reference,
            Provincia = request.Provincia,
            Canton = request.Canton,
            Parroquia = request.Parroquia,
        };
        await _context.Address.AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _addresCreateResponse = _mapper.Map(address, _addresCreateResponse);
        _addresCreateResponse.PersonId = address.PersonId;
        return _addresCreateResponse;
    }
}
