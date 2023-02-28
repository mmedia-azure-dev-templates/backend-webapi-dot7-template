using Boilerplate.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    [EnumMember(Value = "Cedula")]
    [Display(Name = "Cedula")]
    Cedula,
    [EnumMember(Value = "Dni")]
    [Display(Name = "Dni")]
    Dni,
    [EnumMember(Value = "Pasaporte")]
    [Display(Name = "Pasaporte")]
    Passport
}