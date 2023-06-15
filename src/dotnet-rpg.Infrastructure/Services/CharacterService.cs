using AutoMapper;
using dotnet_rpg.Application.Dtos.Character;
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

    private readonly IMapper _mapper;

    public CharacterService(
        ApplicationDbContext db,
        IMapper mapper)
    {
        _db = db;
        _dbSet = _db.Set<Character>();
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            return new ServiceResponse<GetCharacterResponseDto>
            {
                Data = _mapper.Map<GetCharacterResponseDto>(entity),
            };
        }
        else
        {
            return new ServiceResponse<GetCharacterResponseDto>
            {
                Success = false,
                Message = "Entity not found"
            };
        }
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
    {
        return new ServiceResponse<List<GetCharacterResponseDto>>
        {
            Data = await _dbSet.Select(c =>
                    _mapper.Map<GetCharacterResponseDto>(c))
                .ToListAsync(),
        };
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
    {
        var entity = _mapper.Map<Character>(newCharacter);
        entity.Id = Guid.NewGuid();
        
        await _dbSet.AddAsync(entity);
        await _db.SaveChangesAsync(CancellationToken.None);
        
        return new ServiceResponse<List<GetCharacterResponseDto>>
        {
            Data = await _dbSet.Select(c =>
                    _mapper.Map<GetCharacterResponseDto>(c))
                .ToListAsync(),
        };
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacterById(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync(CancellationToken.None);

            return new ServiceResponse<List<GetCharacterResponseDto>>
            {
                Data = await _dbSet.Select(c =>
                        _mapper.Map<GetCharacterResponseDto>(c))
                    .ToListAsync(),
            };
        }
        else
        {
            return new ServiceResponse<List<GetCharacterResponseDto>>
            {
                Success = false,
                Message = "Entity not found"
            };
        }
    }
}