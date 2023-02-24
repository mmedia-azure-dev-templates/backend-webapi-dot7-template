using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Features.Auth.GenerateInitial;
public class GenerateInitialConfirmationRequest : IRequest<GenerateInitialConfirmationResponse>
{
    [Required]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string Email { get; set; }
    public string Token { get; set; }
}
