using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentMethodsType
{
    [Display(Name = "Pago de Contado")]
    CashPayment,
    [Display(Name = "Crédito Directo")]
    DirectCredit,
    [Display(Name = "FCME")]
    Fcme,
    [Display(Name = "Anticipo")]
    CashAdvance,
    [Display(Name = "Tarjeta Crédito")]
    CreditCard,
}