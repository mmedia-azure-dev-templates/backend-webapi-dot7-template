﻿using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.EditUser;

public record EditUserRequest : IRequest<EditUserResponse>
{
    public UserId UserId { get; set; }
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public DocumentType DocumentType { get; init; }
    public NacionalityType NacionalityType { get; init; }
    public string Ndocument { get; init; } = null!;
    public GenderType GenderType { get; init; }
    public CivilStatusType CivilStatusType { get; init; }
    public DateTime BirthDate { get; init; }
    public string Mobile { get; init; }
    public string PrimaryStreet { get; init; }
    public string SecondaryStreet { get; init; }
    public string Numeration { get; init; }
    public string Reference { get; init; }
    public int Provincia { get; init; }
    public int Canton { get; init; }
    public int Parroquia { get; init; }
    public string ImageProfile { get; init; }
}