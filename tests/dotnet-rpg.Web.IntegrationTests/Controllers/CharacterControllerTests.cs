using System.Net;
using System.Net.Http.Json;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Web.IntegrationTests.Controllers;

public class CharacterControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task GetAllCharacters_ShouldReturns_Ok200_ServiceResponse_ListOf_GetCharacterResponseDto()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        //Act
        var response = await client.GetAsync("api/Character");

        var serviceResponse =
            await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetCharacterResponseDto>>>();
        var entities = serviceResponse?.Data;

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        serviceResponse.Should().NotBeNull();
        serviceResponse?.Data.Should().NotBeNullOrEmpty();

        entities.Should().NotBeNull();
        entities.Should().AllBeOfType<GetCharacterResponseDto>();
        entities?.Count.Should().Be(4);
    }

    [Fact]
    public async Task GetCharacterById_ShouldReturns_Ok200_ServiceResponse_GetCharacterResponseDto()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var expected = new GetCharacterResponseDto
        {
            Id = Guid.Parse("6D7E88B8-A1D7-46EC-AF53-C2469E22275F"),
            Name = "Sara Connor",
            HitPoints = 100,
            Intelligence = 5,
            Strength = 10,
            Defense = 10,
            Class = RpgClass.Knight
        };

        var id = expected.Id;

        //Act
        var response = await client.GetAsync($"api/Character/{id}");

        var serviceResponse =
            await response.Content.ReadFromJsonAsync<ServiceResponse<GetCharacterResponseDto>>();
        var entity = serviceResponse?.Data;

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        serviceResponse.Should().NotBeNull();
        serviceResponse?.Data.Should().NotBeNull();

        entity.Should().NotBeNull();
        entity.Should().BeOfType<GetCharacterResponseDto>();
        entity.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetCharacterByNotExistingId_ShouldReturns_NotFound404_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var id = Guid.NewGuid();

        //Act
        var response = await client.GetAsync($"api/Character/{id}");

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(404);
        exception.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task GetCharacterByIncorrectId_ShouldReturns_BadRequest400_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var id = "1D44225E-1689-4220-8EB7";

        //Act
        var response = await client.GetAsync($"api/Character/{id}");

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(400);
        exception.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task CreateCharacter_ShouldReturns_Ok200_ServiceResponse_ListOf_GetCharacterResponseDto()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var created = new AddCharacterRequestDto()
        {
            Name = "Mick Gordon",
            HitPoints = 666,
            Intelligence = 10,
            Strength = 10,
            Defense = 10,
            Class = RpgClass.Knight
        };

        //Act
        var response = await client.PostAsJsonAsync("/api/Character", created);

        var serviceResponse =
            await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetCharacterResponseDto>>>();
        var entities = serviceResponse?.Data;

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        serviceResponse.Should().NotBeNull();
        serviceResponse?.Data.Should().NotBeNull();

        entities.Should().NotBeNull();
        entities.Should().AllBeOfType<GetCharacterResponseDto>();
        entities.Should().ContainEquivalentOf(created);
        entities?.Count.Should().Be(5);
    }

    [Fact]
    public async Task CreateCharacterWithIncorrectData_ShouldReturns_BadRequest400_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var created = new AddCharacterRequestDto()
        {
            Name = "",
            HitPoints = -5,
            Intelligence = -10,
            Strength = -10,
            Defense = -10,
            Class = RpgClass.Knight
        };

        //Act
        var response = await client.PostAsJsonAsync("/api/Character", created);

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(400);
        exception.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task UpdateCharacter_ShouldReturns_Ok200_ServiceResponse_GetCharacterResponseDto()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var updated = new UpdateCharacterRequestDto()
        {
            Id = Guid.Parse("862BD1F7-FFB8-4BD3-8875-F9811A29D0C0"),
            Name = "Megumin Updated",
            HitPoints = 5555,
            Intelligence = 1000,
            Strength = 55353,
            Defense = 500,
            Class = RpgClass.Knight
        };

        //Act
        var response = await client.PutAsJsonAsync("/api/Character", updated);

        var serviceResponse =
            await response.Content.ReadFromJsonAsync<ServiceResponse<GetCharacterResponseDto>>();
        var entities = serviceResponse?.Data;

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        serviceResponse.Should().NotBeNull();
        serviceResponse?.Data.Should().NotBeNull();

        entities.Should().NotBeNull();
        entities.Should().BeOfType<GetCharacterResponseDto>();
        entities.Should().BeEquivalentTo(updated);
    }

    [Fact]
    public async Task UpdateCharacterByNotExistingId_ShouldReturns_NotFound404_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var updated = new UpdateCharacterRequestDto()
        {
            Id = Guid.NewGuid(),
            Name = "Megumin Updated",
            HitPoints = 5555,
            Intelligence = 1000,
            Strength = 55353,
            Defense = 500,
            Class = RpgClass.Knight
        };

        //Act
        var response = await client.PutAsJsonAsync("/api/Character", updated);

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(404);
        exception.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task UpdateCharacterWithIncorrectData_ShouldReturns_BadRequest400_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var updated = new UpdateCharacterRequestDto()
        {
            Id = Guid.Parse("862BD1F7-FFB8-4BD3-8875-F9811A29D0C0"),
            Name = "",
            HitPoints = -10,
            Intelligence = -1000,
            Strength = -55353,
            Defense = -500,
            Class = RpgClass.Knight
        };

        //Act
        var response = await client.PutAsJsonAsync("/api/Character", updated);

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(400);
        exception.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task
        DeleteCharacter_ShouldReturns_Ok200_ServiceResponse_ListOf_GetCharacterResponseDto_WithoutDeletedEntity()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var deleted = new UpdateCharacterRequestDto()
        {
            Id = Guid.Parse("862BD1F7-FFB8-4BD3-8875-F9811A29D0C0"),
            Name = "Megumin Updated",
            HitPoints = 5555,
            Intelligence = 1000,
            Strength = 55353,
            Defense = 500,
            Class = RpgClass.Knight
        };

        var id = deleted.Id;

        //Act
        var response = await client.DeleteAsync($"api/Character/{id}");

        var serviceResponse =
            await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetCharacterResponseDto>>>();
        var entities = serviceResponse?.Data;

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert
        entities.Should().NotBeNull();
        entities.Should().AllBeOfType<GetCharacterResponseDto>();
        entities.Should().NotContainEquivalentOf(deleted);
        entities?.Count.Should().Be(3);
    }

    [Fact]
    public async Task DeleteCharacterByNotExistingId_ShouldReturns_NotFound404_ProblemDetails()
    {
        CustomWebApplicationFactory webhost = new();

        //Arrange 
        var client = webhost.CreateClient();

        var id = Guid.NewGuid();
        
        //Act
        var response = await client.DeleteAsync($"api/Character/{id}");

        var exception =
            await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        exception.Should().NotBeNull();
        exception?.Status.Should().Be(404);
        exception.Should().BeOfType<ProblemDetails>();
    }
}