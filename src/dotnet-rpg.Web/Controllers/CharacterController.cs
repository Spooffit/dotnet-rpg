using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(
        ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    /// <summary>
    /// Gets a list of all Characters
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/Character/GetAll
    /// </remarks>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> GetAllCharacters()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    /// <summary>
    /// Gets a Character by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/Character/5C4BFA83-B0BF-43B5-9217-D14042704861
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetCharacter(Guid id)
    {
        var response = await _characterService.GetCharacterById(id);
        if (response.Data is not null)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    /// <summary>
    /// Add a new Character
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/Character
    /// </remarks>
    /// <param name="newCharacter"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    [HttpPost]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }
    
    /// <summary>
    /// Delete a Character
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /api/Character/5C4BFA83-B0BF-43B5-9217-D14042704861
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> DeleteCharacter(Guid id)
    {
        var response = await _characterService.DeleteCharacterById(id);
        if (response.Data is not null)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
    
    /// <summary>
    /// Update a new Character
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /api/Character
    /// </remarks>
    /// <param name="updateCharacter"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpPut]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto updateCharacter)
    {
        var response = await _characterService.UpdateCharacterById(updateCharacter);
        if (response.Data is not null)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
}