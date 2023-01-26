using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Identification
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CatTypeDocument { get; set; }

    public int? CatNacionality { get; set; }

    public string? Ndocument { get; set; }

    public int? CatGender { get; set; }

    public int? CatCivilStatus { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? DepartureDate { get; set; }

    public int? Hired { get; set; }

    public string? ImgUrl { get; set; }

    public string? CurriculumUrl { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? UbcProvincia { get; set; }

    public int? UbcCanton { get; set; }

    public int? UbcParroquia { get; set; }

    public string? Notes { get; set; }
}
