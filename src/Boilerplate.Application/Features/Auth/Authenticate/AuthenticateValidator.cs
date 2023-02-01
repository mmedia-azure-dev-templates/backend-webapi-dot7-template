using FluentValidation;

namespace Boilerplate.Application.Features.Augh.Authenticate;

public class AuthenticateValidator : AbstractValidator<AuthenticateRequest>
{

    public AuthenticateValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}