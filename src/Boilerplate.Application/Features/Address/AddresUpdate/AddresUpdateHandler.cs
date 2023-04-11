using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
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
        //_context.Address.Update(customer);
        Addres addres = new()
        {
            PersonId = request.PersonId,
            PrimaryStreet = request.PrimaryStreet,
            SecondaryStreet = request.SecondaryStreet,
            Numeration = request.Numeration,
            Reference = request.Reference,
            Provincia = request.Provincia,
            Canton = request.Canton,
            Parroquia = request.Parroquia,
            Notes = request.Notes,
        };

        _context.Address.Add(addres);
        await _context.SaveChangesAsync(cancellationToken);
        return _addresUpdateResponse;
    }
}
