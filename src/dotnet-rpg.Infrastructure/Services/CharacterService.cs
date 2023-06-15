using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Infrastructure.Persistence;
using dotnet_rpg.Web.Services;
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

    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
        return new ServiceResponse<List<Character>>
        {
            Data = await _dbSet.ToListAsync()
        };
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            return new ServiceResponse<Character>
            {
                Data = entity,
            };
        }
        else
        {
            return new ServiceResponse<Character>
            {
                Success = false,
                Message = "Entity not found"
            };
        }
    }

    public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
    {
        await _dbSet.AddAsync(newCharacter);
        await _db.SaveChangesAsync(CancellationToken.None);
        return new ServiceResponse<List<Character>>
        {
            Data = await _dbSet.ToListAsync(),
        };
    }

    public async Task<ServiceResponse<List<Character>>> DeleteCharacterById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync(CancellationToken.None);
            
            return new ServiceResponse<List<Character>>
            {
                Data = await _dbSet.ToListAsync(),
            };
        }
        else
        {
            return new ServiceResponse<List<Character>>
            {
                Success = false,
                Message = "Entity not found"
            };
        }
    }
}