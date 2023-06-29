using dotnet_rpg.Application.Dtos.Character;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Web.Controllers;

[ApiController]
[ApiExceptionFilter]
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
    /// GET /api/Character
    /// </remarks>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> 
        Get()
    {
        var response = new ServiceResponse<List<GetCharacterResponseDto>>();
        response = await _characterService.GetAllCharactersAsync();
        return Ok(response);
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
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> 
        Get(Guid id)
    {
        var response = new ServiceResponse<GetCharacterResponseDto>();
        response = await _characterService.GetCharacterByIdAsync(id);
        
        return Ok(response);
    }

    /// <summary>
    /// Creates a new Character
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/Character
    /// </remarks>
    /// <param name="newCharacter"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(string),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> 
        Create([FromBody]AddCharacterRequestDto newCharacter)
    {
        var response = new ServiceResponse<List<GetCharacterResponseDto>>();
        response = await _characterService.AddCharacterAsync(newCharacter);
        
        return Created(nameof(GetCharacterResponseDto),response);
    }
    
    /// <summary>
    /// Updates a Character
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /api/Character
    /// </remarks>
    /// <param name="updateCharacter"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> 
        Update([FromBody]UpdateCharacterRequestDto updateCharacter)
    {
        var response = new ServiceResponse<GetCharacterResponseDto>();
        response = await _characterService.UpdateCharacterAsync(updateCharacter);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Deletes a Character id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /api/Character/5C4BFA83-B0BF-43B5-9217-D14042704861
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>Returns ServiceResponse</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> 
        Delete(Guid id)
    {
        var response = new ServiceResponse<List<GetCharacterResponseDto>>();
        response = await _characterService.DeleteCharacterByIdAsync(id);
        
        return Ok(response);
    }
}