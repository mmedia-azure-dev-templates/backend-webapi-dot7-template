using FluentValidation;

namespace Boilerplate.Application.Features.Teams.Create;

public class CreateTeamValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        /*RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255);*/
        
    }
}