using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.Domain.Asset.Features;

public record CreateAssetCommand(NewAssetDto Asset) : IRequest<Result<AssetDto>>;

public class CreateAssetHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateAssetCommand, Result<AssetDto>>
{
    public async Task<Result<AssetDto>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var newAsset = mapper.Map<Asset>(request.Asset);
        var newTags = request.Asset.Tags.Select(t => new AssetTag
        {
            Tag = t,
            AssetId = newAsset.Id
        });
        
        newAsset.Tags.AddRange(newTags);
        unitOfWork.Context.Assets.Add(newAsset);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset.");
        return mapper.Map<AssetDto>(newAsset);
    }
}