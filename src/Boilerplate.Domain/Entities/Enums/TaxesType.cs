using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaxesType
{
    [Display(Name = "IVA")]
    Iva,
    [Display(Name = "ICE")]
    Ice,
}