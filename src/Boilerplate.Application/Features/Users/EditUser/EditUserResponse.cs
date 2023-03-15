using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.Users.EditUser;

public record EditUserResponse: IEditUserResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;
    public EditUserResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}