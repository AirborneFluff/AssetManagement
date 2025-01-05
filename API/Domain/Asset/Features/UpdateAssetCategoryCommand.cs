using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record UpdateAssetCategoryCommand(string CategoryId, NewAssetCategoryDto Category) : IRequest<Result<AssetCategoryDto>>;

public class UpdateAssetCategoryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateAssetCategoryCommand, Result<AssetCategoryDto>>
{
    public async Task<Result<AssetCategoryDto>> Handle(UpdateAssetCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Context.AssetCategories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (category is null) return Result.Fail("Category not found");
        
        mapper.Map(request.Category, category);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset category.");
        return mapper.Map<AssetCategoryDto>(category);
    }
}