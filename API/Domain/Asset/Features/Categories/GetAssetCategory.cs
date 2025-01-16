using API.Data.Interfaces;
using API.Domain.Asset.Dto.Categories;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Categories;

public record GetAssetCategory(string CategoryId) : IRequest<Result<AssetCategoryDto>>;

public class GetAssetCategoryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAssetCategory, Result<AssetCategoryDto>>
{
    public async Task<Result<AssetCategoryDto>> Handle(GetAssetCategory request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Context.AssetCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(asset => asset.Id == request.CategoryId, cancellationToken: cancellationToken);
        
        if (result is null) return Result.Fail("Category not found");
        return mapper.Map<AssetCategoryDto>(result);
    }
}