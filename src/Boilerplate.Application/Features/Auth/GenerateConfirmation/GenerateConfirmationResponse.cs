﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.GenerateConfirmation;
public class GenerateConfirmationResponse
{
    public string Message { get; set; } = "";
    public bool Transaction { get; set; } = false!;
}
