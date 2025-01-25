using API.Data.Interfaces;
using API.Domain.Shared.Helpers;
using API.Domain.Shared.Params;
using API.Domain.Tenant.DTOs;
using API.Extensions;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Tenant.Features;

public record GetTenantsCommand(SortableParams PageParams) : IRequest<Result<PagedList<TenantDto>>>;

public class GetTenantsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetTenantsCommand, Result<PagedList<TenantDto>>>
{
    public async Task<Result<PagedList<TenantDto>>> Handle(GetTenantsCommand request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Context.Tenants
            .AsNoTracking()
            .AsQueryable()
            .OrderByField(request.PageParams.SortField, request.PageParams.SortOrder);

        var results = await PagedList<AppTenant>
            .CreateAsync(query, request.PageParams);
        
        return mapper.Map<PagedList<TenantDto>>(results);
    }
}