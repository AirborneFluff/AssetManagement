using API.Data.Interfaces;
using API.Domain.Asset.Dto.SupplySources;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.SupplySources;

public record UpdateSupplySourceCommand(string SupplySourceId, NewAssetSupplySourceDto SupplySource) : IRequest<Result<AssetSupplySourceDto>>;

public class UpdateSupplySourceHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateSupplySourceCommand, Result<AssetSupplySourceDto>>
{
    public async Task<Result<AssetSupplySourceDto>> Handle(UpdateSupplySourceCommand request, CancellationToken cancellationToken)
    {
        var supplySource = await unitOfWork.Context.AssetSupplySources
            .FirstOrDefaultAsync(s => s.Id == request.SupplySourceId, cancellationToken);
        if (supplySource is null) return Result.Fail("Supply source not found");
        
        mapper.Map(request.SupplySource, supplySource);
        unitOfWork.Context.Entry(supplySource).State = EntityState.Modified;
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset supply source.");
        return mapper.Map<AssetSupplySourceDto>(supplySource);
    }
}