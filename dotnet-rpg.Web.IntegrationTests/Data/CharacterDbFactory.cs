using dotnet_rpg.Core.Entities;
using dotnet_rpg.Core.Enums;
using dotnet_rpg.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Web.IntegrationTests.Data;

public static class CharacterDbFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        context.Character.AddRange(
            new Character()
            {
                Id = Guid.Parse("A4A94094-0D09-4314-BE20-A1632D475D09"),
                Name = "Test1",
                HitPoints = 100,
                Strength = 10,
                Intelligence = 5,
                Defense = 10,
                Class = RpgClass.Knight
            },
            new Character()
            {
                Id = Guid.Parse("709837FC-CF8E-4A85-9A85-37F9FEBA9FC1"),
                Name = "Test2",
                HitPoints = 100,
                Strength = 10,
                Intelligence = 5,
                Defense = 10,
                Class = RpgClass.Knight
            },
            new Character()
            {
                Id = Guid.Parse("C80A0FFF-7D21-4240-983A-498CBDDCC04A"),
                Name = "Test3",
                HitPoints = 80,
                Strength = 2,
                Intelligence = 10,
                Defense = 4,
                Class = RpgClass.Mage
            },
            new Character()
            {
                Id = Guid.Parse("AB9CADB7-AEDE-42E3-A58D-9A09DD62D6A7"),
                Name = "Test4",
                HitPoints = 60,
                Strength = 6,
                Intelligence = 6,
                Defense = 6,
                Class = RpgClass.Assassin
            }
        );
        context.SaveChanges();
        return context;
    }
    public static List<Character> Characters => new()
    {
        new Character()
        {
            Id = Guid.Parse("A4A94094-0D09-4314-BE20-A1632D475D09"),
            Name = "Test1",
            HitPoints = 100,
            Strength = 10,
            Intelligence = 5,
            Defense = 10,
            Class = RpgClass.Knight
        },
        new Character()
        {
            Id = Guid.Parse("709837FC-CF8E-4A85-9A85-37F9FEBA9FC1"),
            Name = "Test2",
            HitPoints = 100,
            Strength = 10,
            Intelligence = 5,
            Defense = 10,
            Class = RpgClass.Knight
        },
        new Character()
        {
            Id = Guid.Parse("C80A0FFF-7D21-4240-983A-498CBDDCC04A"),
            Name = "Test3",
            HitPoints = 80,
            Strength = 2,
            Intelligence = 10,
            Defense = 4,
            Class = RpgClass.Mage
        },
        new Character()
        {
            Id = Guid.Parse("AB9CADB7-AEDE-42E3-A58D-9A09DD62D6A7"),
            Name = "Test4",
            HitPoints = 60,
            Strength = 6,
            Intelligence = 6,
            Defense = 6,
            Class = RpgClass.Assassin
        },
    };
    
    public static void CleanUp(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}