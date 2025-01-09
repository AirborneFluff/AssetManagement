using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using AutoMapper;
using FluentResults;
using MediatR;

namespace API.Domain.Asset.Features.Suppliers;

public record CreateSupplierCommand(NewAssetSupplierDto Supplier) : IRequest<Result<AssetSupplierDto>>;

public class CreateSupplierHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateSupplierCommand, Result<AssetSupplierDto>>
{
    public async Task<Result<AssetSupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var newAssetSupplier = mapper.Map<AssetSupplier>(request.Supplier);
        unitOfWork.Context.AssetSuppliers.Add(newAssetSupplier);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding asset supplier.");
        return mapper.Map<AssetSupplierDto>(newAssetSupplier);
    }
}