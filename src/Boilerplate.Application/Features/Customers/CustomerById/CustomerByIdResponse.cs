using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;

namespace Boilerplate.Application.Features.Customers.CustomerById;
public class CustomerByIdResponse
{
    public CustomerId? Id { get; set; } = null;
    public IdentificationType? DocumentType { get; init; } = null;
    public string? Ndocument { get; init; } = null;
    public DateTime? BirthDate { get; init; } = null;
    public GenderType? GenderType { get; init; } = null;
    public CivilStatusType? CivilStatusType { get; init; } = null;
    public string? FirstName { get; init; } = null;
    public string? LastName { get; init; } = null;
    public string? Email { get; init; } = null;
    public string? Mobile { get; init; } = null;
    public string? Phone { get; init; } = null;
    public string? PrimaryStreet { get; init; } = null;
    public string? SecondaryStreet { get; init; } = null;
    public string? Numeration { get; init; } = null;
    public string? Reference { get; init; } = null;
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string? Notes { get; init; } = null;
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}