using System.Security.Claims;
using API.Domain.Authentication.Dtos;
using API.Domain.Authentication.Features;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var command = new LoginUserCommand(dto.Email, dto.Password);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        return Ok(AppUserDto.FromClaims(User));
    }
    
    [HttpPost("switchTenant")]
    [Authorize(Roles = "SuperUser")]
    public async Task<IActionResult> SwitchTenant([FromQuery] string tenantId)
    {
        var command = new SwitchTenantCommand(User.FindFirstValue(ClaimTypes.NameIdentifier)!, tenantId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}