using System.Text.Json.Serialization;

namespace Boilerplate.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HeroType
{
    Student,
    Teacher,
    ProHero,
    Villain,
    Vigilante
}