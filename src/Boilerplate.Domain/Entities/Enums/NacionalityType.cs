using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NacionalityType
{
    [Display(Name = "Ecuatoriana")]
    Ecuatoriana,
    [Display(Name = "Venezolana")]
    Venezolana,
    [Display(Name = "Cubana")]
    Cubana
}