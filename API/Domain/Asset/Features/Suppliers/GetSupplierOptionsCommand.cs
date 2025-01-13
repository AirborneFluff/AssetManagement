using API.Data.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record GetSupplierOptionsCommand() : IRequest<Result<Dictionary<string, string>>>;

public class GetSupplierOptionsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSupplierOptionsCommand, Result<Dictionary<string, string>>>
{
    public async Task<Result<Dictionary<string, string>>> Handle(GetSupplierOptionsCommand request, CancellationToken cancellationToken)
    {
        var suppliers = await unitOfWork.Context.AssetSuppliers
            .ToDictionaryAsync(supplier => supplier.Id, supplier => supplier.Name, cancellationToken);
        return Result.Ok(suppliers);
    }
}