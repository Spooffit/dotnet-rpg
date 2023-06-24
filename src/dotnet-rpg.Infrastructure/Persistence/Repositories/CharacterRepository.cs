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

    public async Task AddCharacterAsync(Character? entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteCharacterByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
    }

    public void UpdateCharacter(Character? entity)
    {
        _dbSet.Update(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }
}