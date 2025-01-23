using API.Data.Interfaces;
using API.Domain.Modules;
using API.Domain.Tenant.DTOs;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Tenant.Features;

public record CreateTenantCommand(NewTenantDto Tenant) : IRequest<Result<TenantDto>>;

public class CreateTenantHandler(
    IUnitOfWork unitOfWork, 
    IMapper mapper) : IRequestHandler<CreateTenantCommand, Result<TenantDto>>
{
    public async Task<Result<TenantDto>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantNameTaken = await unitOfWork.Context.Tenants
            .AnyAsync(t => t.Name.ToLower() == request.Tenant.Name.ToLower(), cancellationToken);
        if (tenantNameTaken) return Result.Fail("Tenant name is taken.");
        
        var newTenant = mapper.Map<AppTenant>(request.Tenant);

        var moduleSettings = AppModuleSettings
            .GetAllSettings()
            .Where(m => m.AutomaticallyEnabled)
            .Select(m => m.Identifier)
            .ToList();
        
        var modules = await unitOfWork.Context.Modules
            .Where(m => moduleSettings.Contains(m.Identifier))
            .ToListAsync(cancellationToken: cancellationToken);
        
        newTenant.Modules.AddRange(modules);
        unitOfWork.Context.Tenants.Add(newTenant);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue adding tenant.");
        return mapper.Map<TenantDto>(newTenant);
    }
}