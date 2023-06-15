using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Application.Services;

public interface ICharacterService
{
    Task<List<Character>> GetAllCharacters();
    Task<Character> GetCharacterById(Guid id);
    Task<List<Character>> AddCharacter(Character newCharacter);
    Task<List<Character>> DeleteCharacterById(Guid id);
    
}