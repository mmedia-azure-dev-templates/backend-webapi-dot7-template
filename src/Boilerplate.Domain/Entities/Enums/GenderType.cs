using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GenderType
{
    [Display(Name = "Masculino")]
    Male,
    [Display(Name = "Femenino")]
    Female
}