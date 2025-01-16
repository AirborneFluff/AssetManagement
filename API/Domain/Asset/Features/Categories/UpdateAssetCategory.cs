using API.Data.Interfaces;
using API.Domain.Asset.Dto.Categories;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Categories;

public record UpdateAssetCategory(string CategoryId, NewAssetCategoryDto Category) : IRequest<Result<AssetCategoryDto>>;

public class UpdateAssetCategoryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateAssetCategory, Result<AssetCategoryDto>>
{
    public async Task<Result<AssetCategoryDto>> Handle(UpdateAssetCategory request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Context.AssetCategories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (category is null) return Result.Fail("Category not found");
        
        mapper.Map(request.Category, category);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset category.");
        return mapper.Map<AssetCategoryDto>(category);
    }
}