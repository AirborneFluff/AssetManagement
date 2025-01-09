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
}