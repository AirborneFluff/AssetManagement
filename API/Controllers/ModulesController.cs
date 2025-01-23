using API.Domain.Modules.Dtos;
using API.Domain.Modules.Features;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ModulesController(IMediator mediator): BaseApiController
{
    [HttpPost]
    [Authorize(Roles = "SuperUser")]
    public async Task<IActionResult> ToggleTenantModule([FromBody]ToggleModuleDto dto)
    {
        var command = new ToggleModuleCommand(dto.TenantId, dto.ModuleIdentifier);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}