using Boilerplate.Application.Features.Addresses.AddressCreate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.Customers.CustomerCreate;
public class CustomerCreateResponse: ICustomerCreateResponse
{
    public bool CustomerComplete { get; set; } = false;
    public CustomerId? CustomerId { get; set; } = null;
    public DocumentType DocumentType { get; init; }
    public string Ndocument { get; init; } = null;
    public DateTime? BirthDate { get; init; }
    public GenderType? GenderType { get; init; }
    public CivilStatusType? CivilStatusType { get; init; }
    public string? FirstName { get; init; } = null!;
    public string? LastName { get; init; } = null!;
    public string? Email { get; init; } = null!;
    public string? Mobile { get; init; }
    public string? Phone { get; init; }
    public AddressCreateResponse? addresCreateResponse { get; set; }
    public string? Notes { get; init; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
