using API.Data.Interfaces;
using API.Domain.Asset.Dto.Suppliers;
using API.Domain.Shared.Helpers;
using API.Domain.Shared.Params;
using API.Extensions;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Suppliers;

public record GetSuppliers(SortableParams PageParams) : IRequest<Result<PagedList<AssetSupplierDto>>>;

public class GetSuppliersHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetSuppliers, Result<PagedList<AssetSupplierDto>>>
{
    public async Task<Result<PagedList<AssetSupplierDto>>> Handle(GetSuppliers request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Context.AssetSuppliers
            .AsNoTracking()
            .AsQueryable()
            .OrderByField(request.PageParams.SortField, request.PageParams.SortOrder);

        var results = await PagedList<AssetSupplier>
            .CreateAsync(query, request.PageParams);
        
        return mapper.Map<PagedList<AssetSupplierDto>>(results);
    }
}