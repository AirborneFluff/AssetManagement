using System.Collections.Concurrent;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Modules;

public class TenantModulesService(IServiceProvider serviceProvider) : ITenantModulesService
{
    ConcurrentDictionary<string, string> _cache { get; set; } = new();

    public async Task<bool> IsModuleVersionValid(string tenantId, string moduleVersion, CancellationToken cancellationToken)
    {
        var cachedVersion = _cache.TryGetValue(tenantId, out var value) ? value : null;
        var version = cachedVersion ?? await GetTenantModuleVersion(tenantId, cancellationToken);
        return moduleVersion == version;
    }

    public void InvalidateModuleVersion(string tenantId)
    {
        _cache.TryRemove(tenantId, out _);
    }

    private async Task<string> GetTenantModuleVersion(string tenantId, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        var moduleVersion = await context.Tenants
            .Where(t => t.Id == tenantId)
            .Select(t => t.ModulesVersion)
            .FirstAsync(cancellationToken: cancellationToken);
        
        _cache.TryAdd(tenantId, moduleVersion);
        return moduleVersion;
    }
}