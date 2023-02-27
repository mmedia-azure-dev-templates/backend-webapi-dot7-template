using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Entities.Enums;
public enum CivilStatusType
{
    [Display(Name = "Casado")]
    Married,
    [Display(Name = "Divorciado")]
    Divorced,
    [Display(Name = "Soltero")]
    Single,
    [Display(Name = "Unio Libre")]
    FreeUnion,
    [Display(Name = "Viudo")]
    Widower
}
