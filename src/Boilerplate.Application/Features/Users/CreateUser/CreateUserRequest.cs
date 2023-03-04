using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.Graph;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Features.Users.CreateUser;

public record CreateUserRequest : IRequest<UserResponse>
{
    public string Email { get; init; } = null!;  
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}

public record CreateUsersInformationsRequest : IRequest<UserResponse>
{
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public IdentificationType IdentificationType { get; init; }
    public NacionalityType NacionalityType { get; init; }
    public string Ndocument { get; init; } = null!;
    public GenderType GenderType { get; init; }
    public CivilStatusType CivilStatusType { get; init; }
    public DateTime? BirthDate { get; init; }
    public string? Mobile { get; init; }
    public string PrimaryStreet { get; init; }
    public string SecondaryStreet { get; init; }
    public string Numeration { get; init; }
    public string Reference { get; init; }
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string ImageProfile { get; init; }
}