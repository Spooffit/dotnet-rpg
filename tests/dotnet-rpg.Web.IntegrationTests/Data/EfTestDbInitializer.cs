using dotnet_rpg.Infrastructure.Persistence;

namespace dotnet_rpg.Web.IntegrationTests.Data;

public class EfTestDbInitializer
{
    private readonly ApplicationDbContext _context;

    public EfTestDbInitializer(ApplicationDbContext context)
    {
        _context = context;
    }
        
    public void InitializeDb()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
            
        _context.AddRange(TestDataFactory.Characters);
        _context.SaveChanges();
    }

    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

}