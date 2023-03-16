using Boilerplate.Application.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, OneOf<bool, UserNotFound>>
{
    private readonly IContext _context;
    private readonly IAwsS3Service _awsS3Service;

    public DeleteUserHandler(IContext context, IAwsS3Service awsS3Service)
    {
        _context = context;
        _awsS3Service = awsS3Service;
    }

    public async Task<OneOf<bool, UserNotFound>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {

        var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null) return new UserNotFound();

        _awsS3Service.DeleteFileAsync("users/" + user.Id.ToString(), "fotoperfil.jpg");

        _context.ApplicationUsers.Remove(user!);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}