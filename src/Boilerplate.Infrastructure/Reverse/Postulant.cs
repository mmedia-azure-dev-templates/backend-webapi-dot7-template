using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Postulant
{
    public int Id { get; set; }

    public bool? Contacted { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public int? CatTypeDocument { get; set; }

    public int? CatNacionality { get; set; }

    public string? Ndocument { get; set; }

    public int? Gender { get; set; }

    public int? CatCivilStatus { get; set; }

    public DateTime? BirthDate { get; set; }

    public int? UbcProvincia { get; set; }

    public int? UbcCanton { get; set; }

    public int? UbcParroquia { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public int? State { get; set; }

    public string? ImgUrl { get; set; }

    public string? CurriculumUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
