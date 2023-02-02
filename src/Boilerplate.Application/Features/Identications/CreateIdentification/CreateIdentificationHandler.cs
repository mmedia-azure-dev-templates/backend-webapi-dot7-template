using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Boilerplate.Application.Features.Identications;

namespace Boilerplate.Application.Features.Identications.CreateIdentification;

public class CreateIdentificationHandler : IRequestHandler<CreateIdentificationRequest, IdentificationResponse?>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    
    
    public CreateHeroHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IdentificationResponse?> Handle(CreateIdentificationRequest request, CancellationToken cancellationToken)
    {
        return new IdentificationResponse();
        // var created = _mapper.Map<Domain.Entities.Hero>(request);
        // _context.Heroes.Add(created);
        // await _context.SaveChangesAsync(cancellationToken);
        // return _mapper.Map<IdentificationResponse?>(created);
    }
}