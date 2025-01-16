using API.Domain.Asset.Dto.Categories;
using API.Domain.Asset.Features.Categories;
using API.Domain.Asset.Params;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public partial class AssetsController
{
    [HttpPost("Categories")]
    public async Task<IActionResult> CreateCategory([FromBody] NewAssetCategoryDto dto)
    {
        var command = new CreateAssetCategory(dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpGet("Categories/{categoryId}")]
    public async Task<IActionResult> GetCategory(string categoryId)
    {
        var command = new GetAssetCategory(categoryId);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpPut("Categories/{categoryId}")]
    public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] NewAssetCategoryDto dto)
    {
        var command = new UpdateAssetCategory(categoryId, dto);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    
    [HttpGet("Categories")]
    public async Task<IActionResult> GetAssetCategories([FromQuery]GetAssetCategoriesParams pageParams)
    {
        var command = new GetAssetCategories(pageParams);
        var result = await mediator.Send(command);
        if (result.IsFailed) return BadRequest(result.Errors);
        
        Response.AddPaginationHeaders(result.Value);
        return Ok(result.Value.Items);
    }
}