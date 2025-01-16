using API.Data.Interfaces;
using API.Domain.Asset.Dto.SupplySources;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.Domain.Asset.Features.SupplySources;

public record CreateSupplySource(string AssetId, NewAssetSupplySourceDto SupplySource) : IRequest<Result<AssetSupplySourceDto>>;

public class CreateSupplySourceHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateSupplySource, Result<AssetSupplySourceDto>>
{
    public async Task<Result<AssetSupplySourceDto>> Handle(CreateSupplySource request, CancellationToken cancellationToken)
    {
        var newSupplySource = mapper.Map<AssetSupplySource>(request.SupplySource);
        newSupplySource.AssetId = request.AssetId;
        unitOfWork.Context.AssetSupplySources.Add(newSupplySource);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset supply source.");
        return mapper.Map<AssetSupplySourceDto>(newSupplySource);
    }
}