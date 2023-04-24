using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Users.GetUsers;
public class GetUsersResponse
{
    public Guid Id { get; set; }
    public UserId UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? LastLogin { get; set; }
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public DocumentType DocumentType { get; set; }
    public NacionalityType Nacionality { get; set; }
    public string Ndocument { get; set; } = null!;
    public GenderType Gender { get; set; }
    public CivilStatusType CivilStatus { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public bool Hired { get; set; }
    public string? ImgUrl { get; set; }
    public string? CurriculumUrl { get; set; }
    public string? Mobile { get; set; }
    public string? Phone { get; set; }
    public string PrimaryStreet { get; set; } = null!;
    public string SecondaryStreet { get; set; } = null!;
    public string Numeration { get; set; } = null!;
    public string Reference { get; set; } = null!;
    public int Provincia { get; set; }
    public int Canton { get; set; }
    public int Parroquia { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
