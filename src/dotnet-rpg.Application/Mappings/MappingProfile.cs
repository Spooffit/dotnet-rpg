using AutoMapper;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Character, GetCharacterResponseDto>();
        CreateMap<AddCharacterRequestDto, Character>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateCharacterRequestDto, Character>();
        CreateMap<UpdateCharacterRequestDto, GetCharacterResponseDto>();
    }
}