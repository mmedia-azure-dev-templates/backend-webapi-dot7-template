using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO
/// </summary>
[Table("Identifications", Schema = "web")]
public partial class Identification : Entity<IdentificationId>
{
    [Required]
    public override IdentificationId Id { get; set; }

    public UserId UserId { get; set; }

    public int CatTypeDocument { get; set; }

    public int CatNacionality { get; set; }

    public string Ndocument { get; set; } = null!;

    public int? CatGender { get; set; }

    public int? CatCivilStatus { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateOnly? EntryDate { get; set; }

    public DateOnly? DepartureDate { get; set; }

    public short Hired { get; set; }

    public string? ImgUrl { get; set; }

    public string? CurriculumUrl { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? UbcProvincia { get; set; }

    public int? UbcCanton { get; set; }

    public int? UbcParroquia { get; set; }

    public string? Notes { get; set; }

    public virtual User User { get; set; } = null!;
}
