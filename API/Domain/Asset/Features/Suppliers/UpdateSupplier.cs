using API.Data.Interfaces;
using API.Domain.Asset.Dto.Suppliers;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record UpdateSupplier(string SupplierId, NewAssetSupplierDto Supplier) : IRequest<Result<AssetSupplierDto>>;

public class UpdateSupplierHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateSupplier, Result<AssetSupplierDto>>
{
    public async Task<Result<AssetSupplierDto>> Handle(UpdateSupplier request, CancellationToken cancellationToken)
    {
        var supplier = await unitOfWork.Context.AssetSuppliers
            .FirstOrDefaultAsync(a => a.Id == request.SupplierId, cancellationToken);
        if (supplier is null) return Result.Fail("Asset Supplier not found");
        
        mapper.Map(request.Supplier, supplier);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue updating asset supplier.");
        return mapper.Map<AssetSupplierDto>(supplier);
    }
}