using Boilerplate.Application.Features.UserInformations;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

public record CreateUserInformationRequest : IRequest<UserInformationResponse>
{
    public int TypeDocument { get; init; }

    public int Nacionality { get; init; }

    public string Ndocument { get; init; } = null!;

    public string Gender { get; init; }

    public string CivilStatus { get; init; }

    public DateTime? BirthDate { get; init; }

    public DateTime? EntryDate { get; init; }

    public DateTime? DepartureDate { get; init; }

    public short Hired { get; init; }

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
}