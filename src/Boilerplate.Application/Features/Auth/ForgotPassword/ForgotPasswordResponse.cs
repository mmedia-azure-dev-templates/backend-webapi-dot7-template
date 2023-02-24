﻿using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public class ForgotPasswordResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;

    //public ForgotPasswordResponse(ISweetAlert sweetAlert)
    //{
    //    SweetAlert = sweetAlert;
    //}

    public bool getTransaction()
    {
        return Transaction;
    }
}