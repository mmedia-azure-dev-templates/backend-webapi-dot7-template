using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Features.Users.UpdateEmail;
public class UpdateEmailRequest : IRequest<UpdateEmailResponse>
{
    public required string Email { get; set; }
    public required string EmailReplace { get; set; }
}
