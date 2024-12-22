using API.Domain.Asset.Dto;
using API.Domain.Asset.Features;
using API.Domain.Asset.Params;
using API.Domain.Shared.Params;
using API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AssetsController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsset([FromBody] NewAssetDto dto)
    {
        var command = new CreateAssetCommand(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAssets([FromQuery]GetAssetsParams pageParams)
    {
        var command = new GetAssetsCommand(pageParams);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        Response.AddPaginationHeaders(result.Value);
        return Ok(result.Value.Items);
    }
}