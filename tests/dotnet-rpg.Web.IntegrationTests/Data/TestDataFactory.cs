using dotnet_rpg.Core.Entities;
using dotnet_rpg.Core.Enums;

namespace dotnet_rpg.Web.IntegrationTests.Data;

public static class TestDataFactory
{
    public static List<Character> Characters => new List<Character>
    {
        new Character
        {
            Id = Guid.Parse("6D7E88B8-A1D7-46EC-AF53-C2469E22275F"),
            Name = "Sara Connor",
            HitPoints = 100,
            Intelligence = 5,
            Strength = 10,
            Defense = 10,
            Class = RpgClass.Knight
        },
        
        new Character
        {
            Id = Guid.Parse("6C232ABC-3BAE-4DCA-A3AC-48C98BFE1018"),
            Name = "Michael Jordan",
            HitPoints = 100,
            Intelligence = 5,
            Strength = 10,
            Defense = 10,
            Class = RpgClass.Knight
        },
        
        new Character
        {
            Id = Guid.Parse("862BD1F7-FFB8-4BD3-8875-F9811A29D0C0"),
            Name = "Megumin",
            HitPoints = 50,
            Intelligence = 10,
            Strength = 5,
            Defense = 5,
            Class = RpgClass.Mage
        },
        
        new Character
        {
            Id = Guid.Parse("654339E0-38D7-4A02-BF1B-8D089F6B32B6"),
            Name = "Corvo Attano",
            HitPoints = 70,
            Intelligence = 7,
            Strength = 7,
            Defense = 7,
            Class = RpgClass.Assassin
        },
        
    };
}