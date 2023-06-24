using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Application.Interfaces.Repositories;

public interface ICharacterRepository
{
    Task<List<Character?>> GetAllCharactersAsync();
    Task<Character?> GetCharacterByIdAsync(Guid id);
    Task AddCharacterAsync(Character? entity);
    Task DeleteCharacterByIdAsync(Guid id);
    void UpdateCharacter(Character? entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}