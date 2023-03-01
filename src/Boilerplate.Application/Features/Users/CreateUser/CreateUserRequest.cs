using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
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
    [EmailAddress]
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public IdentificationType TypeDocument { get; init; }
    public NacionalityType Nacionality { get; init; }
    public string Ndocument { get; init; } = null!;
    public GenderType Gender { get; init; }
    public CivilStatusType CivilStatus { get; init; }
    public DateTime? BirthDate { get; init; }
    public DateTime? EntryDate { get; init; }
    public DateTime? DepartureDate { get; init; }
    public bool Hired { get; init; }
    public string? ImgUrl { get; init; }
    public string? CurriculumUrl { get; init; }
    public string? Mobile { get; init; }
    public string? Phone { get; init; }
    public string PrimaryStreet { get; init; }
    public string SecondaryStreet { get; init; }
    public string Numeration { get; init; }
    public string Reference { get; init; }
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string? Notes { get; init; }
    public string ImageProfile { get; set; }
}