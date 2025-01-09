using API.Domain.Asset.Dto;
using API.Domain.Asset.Features;
using API.Domain.Asset.Params;
using API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public partial class AssetsController
{
    [HttpPost("Categories")]
    public async Task<IActionResult> CreateCategory([FromBody] NewAssetCategoryDto dto)
    {
        var command = new CreateAssetCategoryCommand(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpGet("Categories/{categoryId}")]
    public async Task<IActionResult> GetCategory(string categoryId)
    {
        var command = new GetAssetCategoryCommand(categoryId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpPut("Categories/{categoryId}")]
    public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] NewAssetCategoryDto dto)
    {
        var command = new UpdateAssetCategoryCommand(categoryId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpGet("Categories")]
    public async Task<IActionResult> GetAssetCategories([FromQuery]GetAssetCategoriesParams pageParams)
    {
        var command = new GetAssetCategoriesCommand(pageParams);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        Response.AddPaginationHeaders(result.Value);
        return Ok(result.Value.Items);
    }
}