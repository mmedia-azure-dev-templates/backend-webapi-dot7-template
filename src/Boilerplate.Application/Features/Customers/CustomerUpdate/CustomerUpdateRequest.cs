using Boilerplate.Application.Features.Address.AddresCreate;
using Boilerplate.Application.Features.Address.AddresUpdate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Customers.CustomerUpdate;
public class CustomerUpdateRequest: IRequest<CustomerUpdateResponse>
{
    public CustomerId? CustomerId { get; set; } = null;
    public DocumentType? DocumentType { get; set; }
    public string? Ndocument { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public GenderType? GenderType { get; set; }
    public CivilStatusType? CivilStatusType { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Mobile { get; set; }
    public string? Phone { get; set; }
    public AddressUpdateRequest AddresUpdateRequest { get; set; }
    public string? Notes { get; set; }
}
