using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OwnerFilterType
{
    [Display(Name = "Nombres")]
    FirstName,
    [Display(Name = "Apellidos")]
    LastName,
    [Display(Name = "Número Documento")]
    Ndocumento,
}