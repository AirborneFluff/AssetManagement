using API.Data.Interfaces;
using API.Domain.Asset.Dto.Suppliers;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record GetSupplier(string SupplierId) : IRequest<Result<AssetSupplierDto>>;

public class GetSupplierHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupplier, Result<AssetSupplierDto>>
{
    public async Task<Result<AssetSupplierDto>> Handle(GetSupplier request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Context.AssetSuppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(asset => asset.Id == request.SupplierId, cancellationToken: cancellationToken);
        
        if (result is null) return Result.Fail("Asset Supplier not found");
        return mapper.Map<AssetSupplierDto>(result);
    }
}