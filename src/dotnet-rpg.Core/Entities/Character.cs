using dotnet_rpg.Core.Enums;

namespace dotnet_rpg.Core.Entities;

public class Character
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public RpgClass Class { get; set; }
}