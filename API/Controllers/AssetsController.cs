using API.Domain.Asset.Dto;
using API.Domain.Asset.Features;
using API.Domain.Asset.Params;
using API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public partial class AssetsController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] NewAssetDto dto)
    {
        var command = new CreateAssetCommand(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPut("{assetId}")]
    public async Task<IActionResult> UpdateAsset(string assetId, [FromBody] NewAssetDto dto)
    {
        var command = new UpdateAssetCommand(assetId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAssets([FromQuery]GetAssetsParams pageParams)
    {
        var command = new GetAssetsCommand(pageParams);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        Response.AddPaginationHeaders(result.Value);
        return Ok(result.Value.Items);
    }
    
    [HttpGet("{assetId}")]
    public async Task<IActionResult> GetAsset(string assetId)
    {
        var command = new GetAssetCommand(assetId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }
}