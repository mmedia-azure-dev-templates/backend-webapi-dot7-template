using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.Address.AddresById;
public class AddressByIdResponse
{
    public PersonId PersonId { get; set; }
    public string? PrimaryStreet { get; set; } = null!;
    public string? SecondaryStreet { get; set; } = null!;
    public string? Numeration { get; set; } = null!;
    public string? Reference { get; set; } = null!;
    public int? Provincia { get; set; } = null!;
    public int? Canton { get; set; } = null!;
    public int? Parroquia { get; set; } = null!;
    public string? Notes { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}