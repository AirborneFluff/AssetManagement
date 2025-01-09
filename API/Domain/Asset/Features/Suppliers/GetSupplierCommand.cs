using API.Data.Interfaces;
using API.Domain.Asset.Dto.Suppliers;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record GetSupplierCommand(string SupplierId) : IRequest<Result<AssetSupplierDto>>;

public class GetSupplierHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupplierCommand, Result<AssetSupplierDto>>
{
    public async Task<Result<AssetSupplierDto>> Handle(GetSupplierCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Context.AssetSuppliers
            .AsNoTracking()
            .FirstOrDefaultAsync(asset => asset.Id == request.SupplierId, cancellationToken: cancellationToken);
        
        if (result is null) return Result.Fail("Asset Supplier not found");
        return mapper.Map<AssetSupplierDto>(result);
    }
}