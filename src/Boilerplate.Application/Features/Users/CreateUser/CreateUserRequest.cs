using Boilerplate.Application.Features.Identications;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.CreateUser;

public record CreateUserRequest : IRequest<GetUserResponse>
{
    public string Email { get; init; } = null!;  
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}

// public interface ICreateUsersIdenticationsRequest
// {
//     string Email { get; init; }
//     string FirstName { get; init; }
//     string LastName { get; init; }
//     string PhoneNumber { get; init; }
//     int CatTypeDocument { get; init; }
//     int CatNacionality { get; init; }
//     string Ndocument { get; init; }
//     int? CatGender { get; init; }
//     int? CatCivilStatus { get; init; }
//     DateTime? BirthDate { get; init; }
//     DateTime? EntryDate { get; init; }
//     DateTime? DepartureDate { get; init; }
//     short Hired { get; init; }
//     string? ImgUrl { get; init; }
//     string? CurriculumUrl { get; init; }
//     string? Mobile { get; init; }
//     string? Phone { get; init; }
//     string? Address { get; init; }
//     int? UbcProvincia { get; init; }
//     int? UbcCanton { get; init; }
//     int? UbcParroquia { get; init; }
//     string? Notes { get; init; }
// }

public record CreateUsersIdenticationsRequest : IRequest<GetUserResponse>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public int CatTypeDocument { get; init; }
    public int CatNacionality { get; init; }
    public string Ndocument { get; init; } = null!;
    public int? CatGender { get; init; }
    public int? CatCivilStatus { get; init; }
    public DateTime? BirthDate { get; init; }
    public DateTime? EntryDate { get; init; }
    public DateTime? DepartureDate { get; init; }
    public short Hired { get; init; }
    public string? ImgUrl { get; init; }
    public string? CurriculumUrl { get; init; }
    public string? Mobile { get; init; }
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public int? UbcProvincia { get; init; }
    public int? UbcCanton { get; init; }
    public int? UbcParroquia { get; init; }
    public string? Notes { get; init; }
}