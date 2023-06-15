using dotnet_rpg.Core.Enums;

namespace dotnet_rpg.Application.Dtos.Character;

public class GetCharacterResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public RpgClass Class { get; set; }
}