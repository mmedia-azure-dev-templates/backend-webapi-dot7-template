using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Features.Auth.GenerateConfirmation;
public class GenerateConfirmationRequest : IRequest<GenerateConfirmationResponse>
{
    public required string Email { get; set; }
}
