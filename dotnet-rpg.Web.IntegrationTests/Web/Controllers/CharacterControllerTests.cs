using System.Net;
using System.Net.Http.Json;
using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Services;
using FluentAssertions;

namespace dotnet_rpg.Web.IntegrationTests.Web.Controllers;

public class CharacterControllerTests
{
    private readonly HttpClient _httpClient;
    
    public CharacterControllerTests()
    {
        CustomWebApplicationFactory factory = new ();
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Get_AllCharacters_ReturnOk200()
    {
        // Arrange
        // Act
        var response = await _httpClient.GetAsync("api/Character");
        var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetCharacterResponseDto>>>();
        var entities = serviceResponse?.Data;

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        serviceResponse.Should().NotBeNull();
        serviceResponse?.Data.Should().NotBeNullOrEmpty();

        entities.Should().NotBeNull();
        entities.Should().AllBeOfType<GetCharacterResponseDto>();
        entities?.Count.Should().Be(4);
    }
}