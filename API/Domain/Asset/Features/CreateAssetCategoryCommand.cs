using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.Domain.Asset.Features;

public record CreateAssetCategoryCommand(NewAssetCategoryDto Category) : IRequest<Result<AssetCategoryDto>>;

public class CreateAssetCategoryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateAssetCategoryCommand, Result<AssetCategoryDto>>
{
    public async Task<Result<AssetCategoryDto>> Handle(CreateAssetCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await unitOfWork.AssetCategoryRepository.AssetCategoryExistsAsync(request.Category.Name);
        if (categoryExists) return Result.Fail("Category already exists");
        
        var newCategory = mapper.Map<AssetCategory>(request.Category);
        unitOfWork.Context.AssetCategories.Add(newCategory);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset category.");
        return mapper.Map<AssetCategoryDto>(newCategory);
    }
}