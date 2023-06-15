using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Services;

public class CharacterService : ICharacterService
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<Character> _dbSet;
    
    public CharacterService(
        ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<Character>();
    }

    
    public async Task<List<Character>> GetAllCharacters()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Character> GetCharacterById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<Character>> AddCharacter(Character newCharacter)
    {
        await _dbSet.AddAsync(newCharacter);
        await _db.SaveChangesAsync(CancellationToken.None);
        return await _dbSet.ToListAsync();
    }

    public async Task<List<Character>> DeleteCharacterById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
            _dbSet.Remove(entity);
        
        await _db.SaveChangesAsync(CancellationToken.None);
        return await _dbSet.ToListAsync();
    }
}