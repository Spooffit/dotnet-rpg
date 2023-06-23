using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Application.Interfaces.Repositories;

public interface ICharacterRepository
{
    Task<List<Character?>> GetAllCharactersAsync();
    Task<Character?> GetCharacterByIdAsync(Guid id);
    Task<List<Character?>> AddCharacterAsync(Character? entity, CancellationToken cancellationToken = default);
    Task<List<Character?>> DeleteCharacterByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Character?> UpdateCharacterAsync(Character? entity, CancellationToken cancellationToken = default);
}