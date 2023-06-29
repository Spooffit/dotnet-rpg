using dotnet_rpg.Application.Interfaces;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<Character> Character { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}