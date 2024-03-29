﻿using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("UserInformations", Schema = "web")]
public class UserInformation : Entity<UserInformationId>, IDateCreatedAndUpdated
{
    public override UserInformationId Id { get; set; }
    public UserId UserId { get; set; }
    public DocumentType DocumentType { get; set; }
    public required NacionalityType Nacionality { get; set; }
    public required string Ndocument { get; set; }
    public required GenderType Gender { get; set; }
    public required CivilStatusType CivilStatus { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public required bool Hired { get; set; }
    public string? ImgUrl { get; set; }
    public string? CurriculumUrl { get; set; }
    public string Mobile { get; set; } = null!;
    public string? Phone { get; set; }
    public required string PrimaryStreet { get; set; }
    public required string SecondaryStreet { get; set; }
    public required string Numeration { get; set; }
    public required string Reference { get; set; }
    public required int Provincia { get; set; }
    public required int Canton { get; set; }
    public required int Parroquia { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
