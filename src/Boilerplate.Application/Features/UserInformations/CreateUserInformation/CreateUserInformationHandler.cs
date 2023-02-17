using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.UserInformations.CreateUserInformation;

public class CreateUserInformationHandler : IRequestHandler<CreateUserInformationRequest, UserInformationResponse?>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;


    public CreateUserInformationHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UserInformationResponse?> Handle(CreateUserInformationRequest request, CancellationToken cancellationToken)
    {
        return new UserInformationResponse();
        // var created = _mapper.Map<Domain.Entities.Hero>(request);
        // _context.Heroes.Add(created);
        // await _context.SaveChangesAsync(cancellationToken);
        // return _mapper.Map<IdentificationResponse?>(created);
    }
}