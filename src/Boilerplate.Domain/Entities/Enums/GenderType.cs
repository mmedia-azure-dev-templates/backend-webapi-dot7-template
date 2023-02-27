using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Entities.Enums;
public enum GenderType
{
    [Display(Name = "Masculino")]
    Male,
    [Display(Name = "Femenino")]
    Female
}