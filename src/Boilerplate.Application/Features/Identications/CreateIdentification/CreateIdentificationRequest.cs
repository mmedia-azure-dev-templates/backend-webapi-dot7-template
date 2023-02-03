using Boilerplate.Application.Features.Identications;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

public record CreateIdentificationRequest : IRequest<IdentificationResponse>
{
    public UserId UserId { get; init; }

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