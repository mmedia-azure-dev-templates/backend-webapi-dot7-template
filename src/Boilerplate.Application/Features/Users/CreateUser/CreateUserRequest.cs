using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUsersInformationsRequest : IRequest<UserResponse>
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public DocumentType DocumentType { get; init; }
    public NacionalityType NacionalityType { get; init; }
    public string Ndocument { get; init; } = null!;
    public GenderType GenderType { get; init; }
    public CivilStatusType CivilStatusType { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Mobile { get; init; }
    public string PrimaryStreet { get; init; } = null!;
    public string SecondaryStreet { get; init; } = null!;
    public string Numeration { get; init; } = null!;
    public string Reference { get; init; } = null!;
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string ImageProfile { get; init; } = null!;
    public string Invitation { get; init; } = null!;
}