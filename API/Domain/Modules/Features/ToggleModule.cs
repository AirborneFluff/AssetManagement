using System.Reflection;
using API.Data.Interfaces;
using API.Domain.Tenant;
using API.Services.Modules;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Modules.Features;

public record ToggleModuleCommand(string TenantId, string ModuleIdentifier) : IRequest<Result<bool>>;

public class ToggleModuleHandler(IUnitOfWork unitOfWork, ITenantModulesService modulesService) 
    : IRequestHandler<ToggleModuleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ToggleModuleCommand request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Context.Tenants
            .Include(t => t.Modules)
            .FirstOrDefaultAsync(t => t.Id == request.TenantId, cancellationToken);
        if (tenant is null) return Result.Fail($"Tenant {request.TenantId} does not exist.");
        
        var module = unitOfWork.Context.Modules
            .FirstOrDefault(m => m.Identifier == request.ModuleIdentifier);
        if (module is null) return Result.Fail($"Module {request.ModuleIdentifier} does not exist.");

        var moduleStatus = ToggleModule(tenant, module);
        tenant.RefreshModulesVersion();
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue toggling module.");
        modulesService.InvalidateModuleVersion(tenant.Id);
        
        return moduleStatus;
    }

    private bool ToggleModule(AppTenant tenant, AppModule module)
    {
        if (tenant.Modules.Contains(module))
        {
            tenant.Modules.Remove(module);
            return false;
        }
        tenant.Modules.Add(module);
        return true;
    }
}