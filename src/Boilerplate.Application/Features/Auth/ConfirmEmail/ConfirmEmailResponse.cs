﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ConfirmEmail;
public class ConfirmEmailResponse
{
    public string Message { get; set; } = "";
    public bool Transaction { get; set; } = false!;
}
