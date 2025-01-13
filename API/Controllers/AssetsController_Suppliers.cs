using API.Domain.Asset.Dto.Suppliers;
using API.Domain.Asset.Features.Suppliers;
using API.Domain.Shared.Params;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public partial class AssetsController
{
    [HttpPost("Suppliers")]
    public async Task<IActionResult> CreateSupplier([FromBody] NewAssetSupplierDto dto)
    {
        var command = new CreateSupplierCommand(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpPut("Suppliers/{supplierId}")]
    public async Task<IActionResult> UpdateSupplier(string supplierId, [FromBody] NewAssetSupplierDto dto)
    {
        var command = new UpdateSupplierCommand(supplierId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpGet("Suppliers")]
    public async Task<IActionResult> GetSuppliers([FromQuery]SortableParams pageParams)
    {
        var command = new GetSuppliersCommand(pageParams);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        Response.AddPaginationHeaders(result.Value);
        return Ok(result.Value.Items);
    }
    
    [HttpGet("Suppliers/options")]
    public async Task<IActionResult> GetSupplierOptions()
    {
        var command = new GetSupplierOptionsCommand();
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }
    
    [HttpGet("Suppliers/{supplierId}")]
    public async Task<IActionResult> GetSupplier(string supplierId)
    {
        var command = new GetSupplierCommand(supplierId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }
}