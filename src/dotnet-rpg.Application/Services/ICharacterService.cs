using dotnet_rpg.Core.Entities;
using dotnet_rpg.Web.Services;

namespace dotnet_rpg.Application.Services;

public interface ICharacterService
{
    Task<ServiceResponse<List<Character>>> GetAllCharacters();
    Task<ServiceResponse<Character>> GetCharacterById(Guid id);
    Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);
    Task<ServiceResponse<List<Character>>> DeleteCharacterById(Guid id);
    
}