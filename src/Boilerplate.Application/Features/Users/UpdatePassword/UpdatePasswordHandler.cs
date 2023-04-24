using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, UserResponse>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public UpdatePasswordHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Task<UserResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
       // // Guaranteed to be valid, because it comes from the session.
       // var originalUser = await _context.ApplicationUsers
       //     .FirstAsync(x => x.Id == request.Id, cancellationToken);
       //// originalUser.Password = BC.HashPassword(request.Password);
       // _context.ApplicationUsers.Update(originalUser);
       // await _context.SaveChangesAsync(cancellationToken);
       // return _mapper.Map<UserResponse>(originalUser);
    }
}