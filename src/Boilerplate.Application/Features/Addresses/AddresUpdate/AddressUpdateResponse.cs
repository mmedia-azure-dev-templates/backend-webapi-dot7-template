﻿using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.Addresses.AddresUpdate;
public class AddressUpdateResponse : IAddressUpdateResponse
{
    public bool AddressComplete { get; set; } = false;
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