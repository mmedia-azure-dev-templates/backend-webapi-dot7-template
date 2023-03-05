using Boilerplate.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IdentificationType
{
    [EnumMember(Value = "Cedula")]
    [Display(Name = "Cedula")]
    Cedula,
    [EnumMember(Value = "Passport")]
    [Display(Name = "Pasaporte")]
    Passport
}