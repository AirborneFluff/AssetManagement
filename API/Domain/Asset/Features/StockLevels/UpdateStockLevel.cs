using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using API.Domain.Asset.Dto.StockLevels;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.StockLevels;

public record UpdateStockLevelCommand(string AssetId, UpdateStockLevelDto StockLevel) : IRequest<Result<AssetDto>>;

public class UpdateStockLevelHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateStockLevelCommand, Result<AssetDto>>
{
    public async Task<Result<AssetDto>> Handle(UpdateStockLevelCommand request, CancellationToken cancellationToken)
    {
        var asset = await unitOfWork.Context.Assets
            .FirstOrDefaultAsync(a => a.Id == request.AssetId, cancellationToken);
        if (asset is null) return Result.Fail("Asset not found");

        asset.HistoricStockLevels.Add(new AssetStockLevel
        {
            AssetId = asset.Id,
            StockLevel = asset.StockLevel
        });
        asset.StockLevel = request.StockLevel.StockLevel;
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset.");
        return mapper.Map<AssetDto>(asset);
    }
}