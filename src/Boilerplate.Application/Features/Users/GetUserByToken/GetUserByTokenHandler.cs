using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Threading.Tasks;
using static StackExchange.Redis.Role;
using ISession = Boilerplate.Domain.Implementations.ISession;
namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByTokenHandler : IRequestHandler<GetUserByTokenRequest, GetUserByTokenResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public GetUserByTokenHandler(IMapper mapper, IContext context, ISession session)
    {
        _mapper = mapper;
        _context = context;
        _session = session;
    }

    public async Task<GetUserByTokenResponse> Handle(GetUserByTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await (from applicationUser in _context.ApplicationUsers.AsNoTracking()
                            join userInformation in _context.UserInformations.AsNoTracking() on applicationUser.Id equals userInformation.UserId
                            where userInformation.UserId == _session.UserId
                            select new
                            {
                                applicationUser,
                                userInformation,
                            }).FirstOrDefaultAsync(cancellationToken);
        
        return _mapper.Map<GetUserByIdResponse>((result.applicationUser,result.userInformation));
    }
}