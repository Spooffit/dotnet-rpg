using AutoFixture;
using AutoMapper;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Interfaces.Repositories;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace dotnet_rpg.Web.UnitTests.Services;

public class CharacterServiceTests
{
    private readonly Mock<ICharacterRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly Fixture _fixture;

    private readonly ICharacterService _systemUnderTests;

    public CharacterServiceTests()
    {
        _repositoryMock = new Mock<ICharacterRepository>();
        _mapperMock = new Mock<IMapper>();

        _fixture = new();

        _systemUnderTests = new CharacterService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllCharacters_IfExists_ShouldReturns_ServiceResponse_ListOf_GetCharacterResponseDto()
    {
        // Arrange
        int entitiesCount = 2;

        var entities = _fixture
            .CreateMany<Character>(entitiesCount)
            .ToList();

        var mappedEntities = new List<GetCharacterResponseDto>()
        {
            new GetCharacterResponseDto()
            {
                Id = entities[0].Id,
                Name = entities[0].Name,
                HitPoints = entities[0].HitPoints,
                Defense = entities[0].Defense,
                Intelligence = entities[0].Intelligence,
                Strength = entities[0].Strength,
                Class = entities[0].Class
            },
            
            new GetCharacterResponseDto()
            {
                Id = entities[1].Id,
                Name = entities[1].Name,
                HitPoints = entities[1].HitPoints,
                Defense = entities[1].Defense,
                Intelligence = entities[1].Intelligence,
                Strength = entities[1].Strength,
                Class = entities[1].Class
            },
        };

        _repositoryMock.Setup(r => r.GetAllCharactersAsync())!
            .ReturnsAsync(entities)
            .Verifiable();
        
        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.Is<Character>(x =>
                x.Id == entities[0].Id)))
            .Returns(mappedEntities[0])
            .Verifiable();
        
        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.Is<Character>(x =>
                x.Id == entities[1].Id)))
            .Returns(mappedEntities[1])
            .Verifiable();

        // Act
        var result = await _systemUnderTests
            .GetAllCharactersAsync();

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Count.Should().Be(entitiesCount);
        result.Data.Should().AllBeOfType<GetCharacterResponseDto>();

        _repositoryMock.Verify();
        _mapperMock.VerifyAll();
    }

    [Fact]
    public async Task GetById_IfExists_ShouldReturns_ServiceResponse_GetCharacterResponseDto()
    {
        // Arrange
        int entitiesCount = 2;

        var characters = _fixture
            .CreateMany<Character>(entitiesCount)
            .ToList();

        var gettingCharacter = characters[0];
        var gettingCharacterId = Guid.NewGuid();

        gettingCharacter.Id = gettingCharacterId;

        var responseCharacter = new GetCharacterResponseDto()
        {
            Id = gettingCharacter.Id,
            Name = gettingCharacter.Name,
            HitPoints = gettingCharacter.HitPoints,
            Defense = gettingCharacter.Defense,
            Intelligence = gettingCharacter.Intelligence,
            Strength = gettingCharacter.Strength,
            Class = gettingCharacter.Class
        };

        _repositoryMock.Setup(r => r.GetCharacterByIdAsync(It.Is<Guid>(x =>
                x == gettingCharacter.Id)))
            .ReturnsAsync(gettingCharacter)
            .Verifiable();

        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.Is<Character>(x =>
                x.Id == gettingCharacter.Id)))
            .Returns(responseCharacter)
            .Verifiable();

        // Act
        var result = await _systemUnderTests
            .GetCharacterByIdAsync(gettingCharacter.Id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data?.Should().Be(responseCharacter);
        result.Data.Should().BeOfType<GetCharacterResponseDto>();

        _repositoryMock.Verify();
        _mapperMock.Verify();
    }

    [Fact]
    public async Task
        AddCharacter_IfDone_ShouldReturns_ServiceResponse_ListOf_GetCharacterResponseDto_With_AddedCharacter()
    {
        // Arrange;
        var repositoryEntity = _fixture.Create<Character>();
        var repositoryEntityList = new List<Character?> { repositoryEntity };

        var serviceRequestEntity = new AddCharacterRequestDto
        {
            Name = repositoryEntity.Name,
            HitPoints = repositoryEntity.HitPoints,
            Defense = repositoryEntity.Defense,
            Strength = repositoryEntity.Strength,
            Intelligence = repositoryEntity.Intelligence,
            Class = repositoryEntity.Class
        };

        var serviceEntityToMapped = new GetCharacterResponseDto
        {
            Id = repositoryEntity.Id,
            Name = repositoryEntity.Name,
            HitPoints = repositoryEntity.HitPoints,
            Defense = repositoryEntity.Defense,
            Strength = repositoryEntity.Strength,
            Intelligence = repositoryEntity.Intelligence,
            Class = repositoryEntity.Class
        };

        _repositoryMock.Setup(r => r.AddCharacterAsync(It.Is<Character>(x =>
                x.Name == repositoryEntity.Name)))
            .Verifiable();

        _repositoryMock.Setup(r => r.GetAllCharactersAsync())
            .ReturnsAsync(repositoryEntityList)
            .Verifiable();

        _mapperMock.Setup(m => m.Map<Character>(It.Is<AddCharacterRequestDto>(x =>
                x.Name == repositoryEntity.Name)))
            .Returns(repositoryEntity)
            .Verifiable();

        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.Is<Character>(x =>
                x.Name == repositoryEntity.Name)))
            .Returns(serviceEntityToMapped)
            .Verifiable();

        // Act
        var result = await _systemUnderTests
            .AddCharacterAsync(serviceRequestEntity);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data?.Should().ContainSingle(x =>
            x.Name == repositoryEntity.Name);
        result.Data.Should().AllBeOfType<GetCharacterResponseDto>();

        _repositoryMock.Verify();
        _mapperMock.VerifyAll();
    }

    [Fact]
    public async Task
        DeleteCharacterById_IfDone_ShouldReturns_ServiceResponse_ListOf_GetCharacterResponseDto_Without_DeletedCharacter()
    {
        // Arrange;

        var repositoryEntityList = _fixture.CreateMany<Character>(2).ToList();
        var deletedCharacter = repositoryEntityList[0];

        var repositoryResponseEntityList = new List<Character>();
        repositoryResponseEntityList.Add(repositoryEntityList[1]);
        
        var serviceEntityToMapped = new GetCharacterResponseDto
        {
            Id = repositoryResponseEntityList[0].Id,
            Name = repositoryResponseEntityList[0].Name,
            HitPoints = repositoryResponseEntityList[0].HitPoints,
            Defense = repositoryResponseEntityList[0].Defense,
            Strength = repositoryResponseEntityList[0].Strength,
            Intelligence = repositoryResponseEntityList[0].Intelligence,
            Class = repositoryResponseEntityList[0].Class
        };
        

        _repositoryMock.Setup(r => r.DeleteCharacterByIdAsync(It.Is<Guid>(x => 
                x == deletedCharacter.Id)))
            .Verifiable();
        
        _repositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        _repositoryMock.Setup(r => r.GetAllCharactersAsync())!
            .ReturnsAsync(repositoryResponseEntityList)
            .Verifiable();

        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.IsAny<Character>()))
            .Returns(serviceEntityToMapped)
            .Verifiable();

        // Act
        var result = await _systemUnderTests
            .DeleteCharacterByIdAsync(deletedCharacter.Id);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data?.Should().ContainSingle(x =>
            x.Id == repositoryResponseEntityList[0].Id);
        result.Data.Should().BeOfType<List<GetCharacterResponseDto>>();

        _repositoryMock.VerifyAll();
        _mapperMock.VerifyAll();
    }
    
    [Fact]
    public async Task
        UpdateCharacter_IfDone_ShouldReturns_ServiceResponse_GetCharacterResponseDto_With_UpdatedCharacter()
    {
        // Arrange;
        var forUpdateEntity = _fixture.Create<Character>();

        var repositoryUpdatedEntity = new Character()
        {
            Id = forUpdateEntity.Id,
            Name = forUpdateEntity.Name,
            HitPoints = forUpdateEntity.HitPoints + 10,
            Defense = forUpdateEntity.Defense + 10,
            Strength = forUpdateEntity.Strength + 10,
            Intelligence = forUpdateEntity.Intelligence + 10,
            Class = forUpdateEntity.Class
        };

        var serviceRequestEntity = new UpdateCharacterRequestDto
        {
            Id = forUpdateEntity.Id,
            Name = forUpdateEntity.Name,
            HitPoints = forUpdateEntity.HitPoints,
            Defense = forUpdateEntity.Defense,
            Strength = forUpdateEntity.Strength,
            Intelligence = forUpdateEntity.Intelligence,
            Class = forUpdateEntity.Class
        };
        
        var serviceEntityToMapped = new GetCharacterResponseDto
        {
            Id = repositoryUpdatedEntity.Id,
            Name = repositoryUpdatedEntity.Name,
            HitPoints = repositoryUpdatedEntity.HitPoints,
            Defense = repositoryUpdatedEntity.Defense,
            Strength = repositoryUpdatedEntity.Strength,
            Intelligence = repositoryUpdatedEntity.Intelligence,
            Class = repositoryUpdatedEntity.Class
        };
        
        _repositoryMock.Setup(r => r.UpdateCharacter(It.Is<Character>(x => 
                x == forUpdateEntity)))
            .Verifiable();

        _repositoryMock.Setup(r => r.GetCharacterByIdAsync(It.Is<Guid>(x =>
                x == forUpdateEntity.Id)))
            .ReturnsAsync(repositoryUpdatedEntity)
            .Verifiable();
        
        _repositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        _mapperMock.Setup(m => m.Map<Character>(It.Is<UpdateCharacterRequestDto>(x => 
                x.Id == forUpdateEntity.Id)))
            .Returns(forUpdateEntity)
            .Verifiable();
        
        _mapperMock.Setup(m => m.Map<GetCharacterResponseDto>(It.Is<Character>(x => 
                x.Id == serviceEntityToMapped.Id)))
            .Returns(serviceEntityToMapped)
            .Verifiable();

        // Act
        var result = await _systemUnderTests
            .UpdateCharacterAsync(serviceRequestEntity);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().NotBeNull();
        result.Data.Should().BeOfType<GetCharacterResponseDto>();

        _repositoryMock.VerifyAll();
        _mapperMock.VerifyAll();
    }
}