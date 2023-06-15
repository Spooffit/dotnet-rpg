using dotnet_rpg.Core.Entities;
using dotnet_rpg.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly Character _char = new Character
    {
        Class = RpgClass.Knight
    };
    
    [HttpGet]
    public ActionResult<Character> Get()
    {
        return Ok(_char);
    }
}