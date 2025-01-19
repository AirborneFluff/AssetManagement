using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record UpdateAssetCommand(string AssetId, NewAssetDto Asset) : IRequest<Result<AssetDto>>;

public class UpdateAssetHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateAssetCommand, Result<AssetDto>>
{
    public async Task<Result<AssetDto>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await unitOfWork.Context.Assets
            .Include(a => a.Tags)
            .FirstOrDefaultAsync(a => a.Id == request.AssetId, cancellationToken);
        if (asset is null) return Result.Fail("Asset not found");

        if (request.Asset.StockLevel != asset.StockLevel)
        {
            asset.HistoricStockLevels.Add(new AssetStockLevel
            {
                AssetId = asset.Id,
                StockLevel = asset.StockLevel
            });
        }
        
        mapper.Map(request.Asset, asset);
        var incomingTagNames = request.Asset.Tags.ToHashSet();
        var existingTags = asset.Tags.ToList();

        asset.Tags.RemoveAll(t => !incomingTagNames.Contains(t.Tag));

        var newTags = incomingTagNames
            .Except(existingTags.Select(et => et.Tag))
            .Select(tag => new AssetTag
            {
                Tag = tag,
                AssetId = asset.Id
            });

        asset.Tags.AddRange(newTags);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset.");
        return mapper.Map<AssetDto>(asset);
    }
}