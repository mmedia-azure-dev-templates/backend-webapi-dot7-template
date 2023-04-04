using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUserById;
public class GetUserByIdResponse
{
    public Guid Id { get; set; }
    public UserId UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastLogin { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public IdentificationType TypeDocument { get; set; }
    public NacionalityType Nacionality { get; set; }
    public string Ndocument { get; set; }
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
    public string PrimaryStreet { get; set; }
    public string SecondaryStreet { get; set; }
    public string Numeration { get; set; }
    public string Reference { get; set; }
    public int Provincia { get; set; }
    public int Canton { get; set; }
    public int Parroquia { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    //public ApplicationUser applicationUser { get; set; }
    //public UserInformation userInformation { get; set; }
}
