using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            context.Database.EnsureCreated();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        };
    }
}