using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatusType
{
    [Display(Name = "Ingresada")]
    Entered,
    [Display(Name = "Cancelada")]
    Canceled,
    [Display(Name = "Pendiente")]
    Earring,
    [Display(Name = "Pagada")]
    Paid,
    [Display(Name = "Entregada")]
    Delivered
}
