﻿using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Customer.CustomerCreate;
public class CustomerCreateRequest: IRequest<CustomerCreateResponse>
{
    public CustomerId CustomerId { get; set; }
    public IdentificationType IdentificationType { get; init; }
    public string Ndocument { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public GenderType GenderType { get; init; }
    public CivilStatusType CivilStatusType { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Mobile { get; init; }
    public string Phone { get; init; }
    public string PrimaryStreet { get; init; }
    public string SecondaryStreet { get; init; }
    public string Numeration { get; init; }
    public string Reference { get; init; }
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string? Notes { get; init; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
