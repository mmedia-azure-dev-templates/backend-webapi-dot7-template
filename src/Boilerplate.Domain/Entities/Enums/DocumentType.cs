using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    [EnumMember(Value = "Cedula")]
    [Display(Name = "Cédula")]
    Cedula,
    [EnumMember(Value = "Passport")]
    [Display(Name = "Pasaporte")]
    Passport
}