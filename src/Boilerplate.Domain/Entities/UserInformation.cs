using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO
/// </summary>
[Table("UserInformations", Schema = "web")]
public partial class UserInformation : Entity<IdentificationId>, IDateCreatedAndUpdated
{
    [Required]
    public override IdentificationId Id { get; set; }
    
    public UserId UserId { get; set; }

    public string TypeDocument { get; set; }

    public string Nacionality { get; set; }

    public string Ndocument { get; set; } = null!;

    public string Gender { get; set; }

    public string CivilStatus { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? DepartureDate { get; set; }

    public short Hired { get; set; }

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
}
