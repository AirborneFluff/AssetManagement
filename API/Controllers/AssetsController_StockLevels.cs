using API.ActionFilters;
using API.Domain.Asset;
using API.Domain.Asset.Dto.StockLevels;
using API.Domain.Asset.Features.StockLevels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public partial class AssetsController
{
    [HttpPost("{assetId}/stockLevels")]
    [ValidateEntityExists<Asset>("assetId")]
    public async Task<IActionResult> UpdateStockLevel(string assetId, [FromBody] UpdateStockLevelDto dto)
    {
        var command = new UpdateStockLevelCommand(assetId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}