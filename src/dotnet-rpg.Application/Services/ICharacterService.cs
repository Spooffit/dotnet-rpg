using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Web.Services;

namespace dotnet_rpg.Application.Services;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(Guid id);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacterById(Guid id);
    Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacterById(UpdateCharacterRequestDto updateCharacter);
}