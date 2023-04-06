using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderFilterType
{
    [Display(Name = "Orden")]
    OrderNumber,
    [Display(Name = "Cliente")]
    Customer,
}