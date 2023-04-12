using Boilerplate.Application.Features.Address.AddresCreate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Customers.CustomerCreate;
public class CustomerCreateRequest: IRequest<CustomerCreateResponse>
{
    public IdentificationType DocumentType { get; set; }
    public string? Ndocument { get; set; } = null!;
    public DateTime? BirthDate { get; init; }
    public GenderType? GenderType { get; init; }
    public CivilStatusType? CivilStatusType { get; init; }
    public string? FirstName { get; init; } = null!;
    public string? LastName { get; init; } = null!;
    public string? Email { get; init; } = null!;
    public string? Mobile { get; init; }
    public string? Phone { get; init; }
    public AddresCreateRequest addresCreateRequest { get; init; }
    public string? Notes { get; init; }
}
