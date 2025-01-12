using API.Data.Interfaces;
using API.Domain.Asset.Dto.SupplySources;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.SupplySources;

public record CreateSupplySourcePriceCommand(string SupplySourceId, NewAssetSupplySourcePriceDto Price)
    : IRequest<Result<AssetSupplySourcePriceDto>>;

public class CreateSupplySourcePriceHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateSupplySourcePriceCommand, Result<AssetSupplySourcePriceDto>>
{
    public async Task<Result<AssetSupplySourcePriceDto>> Handle(CreateSupplySourcePriceCommand request, CancellationToken cancellationToken)
    {
        var newPrice = mapper.Map<AssetSupplySourcePrice>(request.Price);
        newPrice.SupplySourceId = request.SupplySourceId;
        unitOfWork.Context.AssetSupplySourcePrices.Add(newPrice);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset supply source price.");
        return mapper.Map<AssetSupplySourcePriceDto>(newPrice);
    }
}