using API.Data.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record GetSupplierOptions() : IRequest<Result<Dictionary<string, string>>>;

public class GetSupplierOptionsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupplierOptions, Result<Dictionary<string, string>>>
{
    public async Task<Result<Dictionary<string, string>>> Handle(GetSupplierOptions request, CancellationToken cancellationToken)
    {
        var suppliers = await unitOfWork.Context.AssetSuppliers
            .ToDictionaryAsync(supplier => supplier.Id, supplier => supplier.Name, cancellationToken);
        return Result.Ok(suppliers);
    }
}