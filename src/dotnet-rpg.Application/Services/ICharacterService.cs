using dotnet_rpg.Application.Dtos.Character;

namespace dotnet_rpg.Application.Services;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharactersAsync();
    Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterByIdAsync(Guid id);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacterAsync(AddCharacterRequestDto newCharacter);
    Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacterByIdAsync(Guid id);
    Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacterAsync(UpdateCharacterRequestDto updateCharacter);
}