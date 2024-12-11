using API.Domain.Authentication.Dtos;
using API.Domain.Authentication.Features;
using API.Domain.Tenant.DTOs;
using API.Domain.Tenant.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TenantsController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    [Authorize(Roles = "SuperUser")]
    public async Task<IActionResult> CreateTenant([FromBody] NewTenantDto dto)
    {
        var command = new CreateTenantCommand(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPost("{tenantId}/users")]
    [Authorize(Roles = "SuperUser,Admin")]
    public async Task<IActionResult> CreateUser([FromBody] NewAppUserDto dto, string tenantId)
    {
        var command = new CreateUserCommand(dto, tenantId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}