using API.Data.Interfaces;
using API.Domain.Tenant.DTOs;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Tenant.Features;

public record DeleteTenant(string TenantId) : IRequest<Result<TenantDto>>;

public class DeleteTenantHandler(
    IUnitOfWork unitOfWork, 
    IMapper mapper) : IRequestHandler<DeleteTenant, Result<TenantDto>>
{
    public async Task<Result<TenantDto>> Handle(DeleteTenant request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Context.Tenants
            .FirstOrDefaultAsync(t => t.Id == request.TenantId, cancellationToken);
        if (tenant is null) return Result.Fail("Tenant not found");
        
        unitOfWork.Context.Tenants.Remove(tenant);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue deleting tenant.");
        return Result.Ok();
    }
}