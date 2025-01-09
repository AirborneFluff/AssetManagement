using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record GetAssetCommand(string AssetId) : IRequest<Result<AssetDto>>;

public class GetAssetHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAssetCommand, Result<AssetDto>>
{
    public async Task<Result<AssetDto>> Handle(GetAssetCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Context.Assets
            .AsNoTracking()
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(asset => asset.Id == request.AssetId, cancellationToken: cancellationToken);
        
        if (result is null) return Result.Fail("Asset not found");
        return mapper.Map<AssetDto>(result);
    }
}