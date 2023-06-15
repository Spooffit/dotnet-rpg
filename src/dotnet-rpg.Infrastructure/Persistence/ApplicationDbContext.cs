using dotnet_rpg.Application.Interfaces;
using dotnet_rpg.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Character> Character { get; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}