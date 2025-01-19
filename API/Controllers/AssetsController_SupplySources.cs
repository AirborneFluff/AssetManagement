using API.ActionFilters;
using API.Domain.Asset;
using API.Domain.Asset.Dto.SupplySources;
using API.Domain.Asset.Features.SupplySources;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public partial class AssetsController
{
    [HttpPost("{assetId}/supplySources")]
    [ValidateEntityExists<Asset>("assetId")]
    public async Task<IActionResult> CreateSupplySource(string assetId, [FromBody] NewAssetSupplySourceDto dto)
    {
        var command = new CreateSupplySourceCommand(assetId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPut("{assetId}/supplySources/{supplySourceId}")]
    [ValidateEntityExists<AssetSupplySource>("supplySourceId")]
    public async Task<IActionResult> UpdateSupplySource(string supplySourceId, [FromBody] NewAssetSupplySourceDto dto)
    {
        var command = new UpdateSupplySourceCommand(supplySourceId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpDelete("{assetId}/supplySources/{supplySourceId}")]
    [ValidateEntityExists<AssetSupplySource>("supplySourceId")]
    public async Task<IActionResult> DeleteSupplySource(string supplySourceId)
    {
        var command = new DeleteSupplySourceCommand(supplySourceId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result);
    }
}