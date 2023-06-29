using AutoMapper;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Exceptions;
using dotnet_rpg.Application.Interfaces.Repositories;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Infrastructure.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;

    public CharacterService(
        ICharacterRepository characterRepository,
        IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterByIdAsync(Guid id)
    {
        ServiceResponse<GetCharacterResponseDto> serviceResponse = new();
        var entity = await _characterRepository.GetCharacterByIdAsync(id);

        if (entity is not null)
        {
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(entity);
            return serviceResponse;
        }
        else
        {
            throw new NotFoundException(nameof(Character), id);
        }
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharactersAsync()
    {
        ServiceResponse<List<GetCharacterResponseDto>> serviceResponse = new();

        var entityList = await _characterRepository.GetAllCharactersAsync();

        serviceResponse.Data = entityList.Select(c => 
            _mapper.Map<GetCharacterResponseDto>(c))
            .ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacterAsync(
        AddCharacterRequestDto newCharacter)
    {
        ServiceResponse<List<GetCharacterResponseDto>> serviceResponse = new();

        var entity = _mapper.Map<Character>(newCharacter);
        entity.Id = Guid.NewGuid();

        await _characterRepository.AddCharacterAsync(entity);
        await _characterRepository.SaveChangesAsync();

        var charList = await _characterRepository.GetAllCharactersAsync();

        serviceResponse.Data = charList.Select(c =>
                _mapper.Map<GetCharacterResponseDto>(c))
            .ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacterByIdAsync(Guid id)
    {
        ServiceResponse<List<GetCharacterResponseDto>> serviceResponse = new();
        
        List<Character> entityList;
        try
        {
            await _characterRepository.DeleteCharacterByIdAsync(id);
            await _characterRepository.SaveChangesAsync();

            entityList = await _characterRepository.GetAllCharactersAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new NotFoundException(nameof(Character), id);
        }

        serviceResponse.Data = entityList.Select(c => 
                _mapper.Map<GetCharacterResponseDto>(c))
            .ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacterAsync(
        UpdateCharacterRequestDto updateCharacter)
    {
        ServiceResponse<GetCharacterResponseDto> serviceResponse = new();

        try
        {
            _characterRepository.UpdateCharacter(_mapper.Map<Character>(updateCharacter));
            await _characterRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NotFoundException(nameof(Character), updateCharacter.Id);
        }
        
        var updatedCharacter = await _characterRepository.GetCharacterByIdAsync(updateCharacter.Id);

        serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(updatedCharacter);

        return serviceResponse;
    }
}