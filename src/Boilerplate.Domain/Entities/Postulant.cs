using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA
/// </summary>
public partial class Postulant : Entity<PostulantId>
{
    [Required]
    public override PostulantId Id { get; set; }

    public bool? Contacted { get; set; }

    public string Name { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CatTypeDocument { get; set; }

    public int CatNacionality { get; set; }

    public string Ndocument { get; set; } = null!;

    public int Gender { get; set; }

    public int CatCivilStatus { get; set; }

    public DateOnly BirthDate { get; set; }

    public int UbcProvincia { get; set; }

    public int UbcCanton { get; set; }

    public int UbcParroquia { get; set; }

    public string? Address { get; set; }

    public string Phone { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public int State { get; set; }

    public string? ImgUrl { get; set; }

    public string? CurriculumUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
