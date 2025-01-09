using API.Data.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record DeleteAssetCommand(string AssetId) : IRequest<Result>;

public class DeleteAssetHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<DeleteAssetCommand, Result>
{
    public async Task<Result> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await unitOfWork.Context.Assets
            .FirstOrDefaultAsync(t => t.Id == request.AssetId, cancellationToken);
        if (asset is null) return Result.Fail("Asset not found");
        
        unitOfWork.Context.Assets.Remove(asset);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue deleting asset.");
        return Result.Ok();
    }
}