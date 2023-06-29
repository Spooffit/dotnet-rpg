using System.Runtime.Serialization;
using AutoMapper;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Mappings;
using dotnet_rpg.Core.Entities;

namespace dotnet_rpg.Application.UnitTests.Mappings;

public class MappingTests
{
    private readonly MapperConfiguration _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(MappingProfile));
        });
        _mapper = _configuration.CreateMapper();
    }
    
    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }
    
    [Theory]
    [InlineData(typeof(Character), typeof(GetCharacterResponseDto))]
    [InlineData(typeof(AddCharacterRequestDto), typeof(Character))]
    [InlineData(typeof(UpdateCharacterRequestDto), typeof(Character))]
    [InlineData(typeof(UpdateCharacterRequestDto), typeof(GetCharacterResponseDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }
    
    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return FormatterServices.GetUninitializedObject(type);
    }
}