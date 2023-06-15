using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(
        ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetCharacter(Guid id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<Character>>> GetAllCharacters()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpPost]
    public async Task<ActionResult<List<Character>>> AddCharacter(Character newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Character>> DeleteCharacter(Guid id)
    {
        return Ok(await _characterService.DeleteCharacterById(id));
    }
}