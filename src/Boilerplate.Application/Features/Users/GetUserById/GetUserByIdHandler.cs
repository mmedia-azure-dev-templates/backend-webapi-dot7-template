using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        GetUserByIdResponse response = new GetUserByIdResponse();
        var result = await (from applicationUser in _context.ApplicationUsers.AsNoTracking()
                            join userInformation in _context.UserInformations.AsNoTracking() on applicationUser.Id equals userInformation.UserId
                            where userInformation.UserId == request.Id
                            select new
                            {
                                applicationUser,
                                userInformation,
                            }).FirstOrDefaultAsync(cancellationToken);

        response.userInformation = result.userInformation;
        response.applicationUser = result.applicationUser;
        return response;
    }
}