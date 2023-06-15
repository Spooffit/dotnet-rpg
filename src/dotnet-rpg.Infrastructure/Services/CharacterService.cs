using dotnet_rpg.Application.Interfaces;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Services;

public class CharacterService : ICharacterService
{
    private readonly DbSet<Character> _dbSet;
    
    public CharacterService(
        IApplicationDbContext context)
    {
        _dbSet = context.Character;
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
        return await _dbSet.ToListAsync();
    }

    public async Task<List<Character>> DeleteCharacter(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
            _dbSet.Remove(entity);
        return await _dbSet.ToListAsync();
    }
}