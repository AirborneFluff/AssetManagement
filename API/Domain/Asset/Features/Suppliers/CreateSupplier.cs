using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using API.Domain.Asset.Dto.Suppliers;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.Domain.Asset.Features.Suppliers;

public record CreateSupplier(NewAssetSupplierDto Supplier) : IRequest<Result<AssetSupplierDto>>;

public class CreateSupplierHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateSupplier, Result<AssetSupplierDto>>
{
    public async Task<Result<AssetSupplierDto>> Handle(CreateSupplier request, CancellationToken cancellationToken)
    {
        var newAssetSupplier = mapper.Map<AssetSupplier>(request.Supplier);
        unitOfWork.Context.AssetSuppliers.Add(newAssetSupplier);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset supplier.");
        return mapper.Map<AssetSupplierDto>(newAssetSupplier);
    }
}