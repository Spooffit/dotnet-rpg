using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }
}