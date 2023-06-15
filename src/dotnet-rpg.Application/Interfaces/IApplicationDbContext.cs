using dotnet_rpg.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Character> Character { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}