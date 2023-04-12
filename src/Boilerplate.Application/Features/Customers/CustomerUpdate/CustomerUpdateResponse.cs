using Boilerplate.Application.Features.Address.AddresCreate;
using Boilerplate.Application.Features.Address.AddresUpdate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.Customers.CustomerUpdate;
public class CustomerUpdateResponse: ICustomerUpdateResponse
{
    public CustomerId? CustomerId { get; set; } = null;
    public IdentificationType DocumentType { get; init; }
    public string? Ndocument { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public GenderType? GenderType { get; init; }
    public CivilStatusType? CivilStatusType { get; init; }
    public string? FirstName { get; init; } = null!;
    public string? LastName { get; init; } = null!;
    public string? Email { get; init; } = null!;
    public string? Mobile { get; init; }
    public string? Phone { get; init; }
    public AddresUpdateResponse AddresUpdateResponse { get; set; }
    public string? Notes { get; init; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}