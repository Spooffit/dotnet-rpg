using dotnet_rpg.Application.Services;
using dotnet_rpg.Core.Entities;
using dotnet_rpg.Core.Enums;
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
    public ActionResult<Character> GetCharacter(Guid id)
    {
        return Ok(_characterService.GetCharacterById(id));
    }

    [HttpGet("GetAll")]
    public ActionResult<List<Character>> GetAllCharacters()
    {
        return Ok(_characterService.GetAllCharacters());
    }
    
    [HttpPost]
    public ActionResult<List<Character>> AddCharacter(Character newCharacter)
    {
        return Ok(_characterService.AddCharacter(newCharacter));
    }
    
    [HttpDelete("{id}")]
    public ActionResult<Character> DeleteCharacter(Guid id)
    {
        return Ok(_characterService.DeleteCharacter(id));
    }
}