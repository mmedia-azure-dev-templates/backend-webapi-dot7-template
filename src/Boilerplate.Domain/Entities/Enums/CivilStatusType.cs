using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
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
