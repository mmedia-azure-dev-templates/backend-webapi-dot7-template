﻿using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IForgotPasswordResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; }
    public void InitDefault(ILocalizationService localizationService);
}
