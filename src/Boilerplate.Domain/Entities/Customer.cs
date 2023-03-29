using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using Microsoft.Graph.ExternalConnectors;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENAN LOS CLIENTES
/// </summary>
[Table("Customers", Schema = "web")]
public class Customer : Entity<CustomerId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override CustomerId Id { get; set; }
    public string DataKey { get; set; }
    public IdentificationType DocumentType { get; set; }
    public string Ndocument { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderType GenderType { get; set; }
    public CivilStatusType CivilStatusType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
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
