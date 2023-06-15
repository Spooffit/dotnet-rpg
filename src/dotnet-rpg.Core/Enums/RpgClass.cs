using System.Text.Json.Serialization;

namespace dotnet_rpg.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RpgClass : byte
{
    Knight = 1,
    Mage = 2,
    Assassin = 3
}