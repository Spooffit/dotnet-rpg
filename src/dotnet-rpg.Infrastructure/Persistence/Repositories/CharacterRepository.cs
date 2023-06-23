using dotnet_rpg.Application.Interfaces.Repositories;
using dotnet_rpg.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Persistence.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Character> _dbSet;

    public CharacterRepository(
        ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<Character>();
    }
    
    public async Task<List<Character?>> GetAllCharactersAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Character?> GetCharacterByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<Character?>> AddCharacterAsync(Character? entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return await _dbSet.ToListAsync();
    }

    public async Task<List<Character?>> DeleteCharacterByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);

        await _db.SaveChangesAsync(cancellationToken);
        
        return await _dbSet.ToListAsync();
    }

    public async Task<Character?> UpdateCharacterAsync(Character? entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return await _dbSet.FindAsync(entity);
    }
}