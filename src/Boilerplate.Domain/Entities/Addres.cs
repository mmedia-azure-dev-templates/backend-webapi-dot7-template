using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Address", Schema = "web")]
public class Addres : Entity<AddresId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override AddresId Id { get; set; }
    public string DataKey { get; set; }
    public PersonId PersonId { get; set; }
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
