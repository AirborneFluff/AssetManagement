namespace API.Services.Modules;

public interface ITenantModulesService
{
    Task<bool> IsModuleVersionValid(string tenantId, string moduleVersion, CancellationToken cancellationToken);
    void InvalidateModuleVersion(string tenantId);
    
}