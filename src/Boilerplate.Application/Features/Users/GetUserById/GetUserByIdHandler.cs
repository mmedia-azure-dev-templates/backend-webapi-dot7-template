using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ISession = Boilerplate.Domain.Implementations.ISession;
namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public GetUserByIdHandler(IMapper mapper, IContext context, ISession session)
    {
        _mapper = mapper;
        _context = context;
        _session = session;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await (from applicationUser in _context.ApplicationUsers.AsNoTracking()
                            join userInformation in _context.UserInformations.AsNoTracking() on applicationUser.Id equals userInformation.UserId
                            where userInformation.UserId == request.UserId
                            select new
                            {
                                applicationUser,
                                userInformation,
                            }).FirstOrDefaultAsync(cancellationToken);
        
        return _mapper.Map<GetUserByIdResponse>((result.applicationUser,result.userInformation));
    }
}