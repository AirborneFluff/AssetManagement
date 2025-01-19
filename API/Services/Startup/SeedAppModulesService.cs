using API.Data;
using API.Domain.Modules;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Startup;

public class SeedAppModulesService(DataContext dataContext) : IStartupService
{
    public async Task<Result> Execute()
    {
        var currentModules = await dataContext.Modules.ToListAsync();
        var moduleSettings = AppModuleSettings.GetAllSettings();
        var newModules = GetNewModules(currentModules, moduleSettings);

        if (newModules.Count == 0) return Result.Ok();
        
        dataContext.Modules.AddRange(newModules);
        if (await dataContext.SaveChangesAsync() != newModules.Count)
        {
            return Result.Fail("Failed to add seed modules");
        }

        var enabledNewModules = newModules
            .Where(m => moduleSettings.Exists(s => 
                s.Identifier.Equals(m.Identifier) &&
                s.AutomaticallyEnabled
            ))
            .ToList();
        
        if (enabledNewModules.Count == 0) return Result.Ok();
        var tenants = await dataContext.Tenants.ToListAsync();

        foreach (var tenant in tenants)
        {
            tenant.Modules.AddRange(enabledNewModules);
        }
        
        return await dataContext.SaveChangesAsync() != newModules.Count
            ? Result.Fail("Failed to add seed modules")
            : Result.Ok();
    }

    private List<AppModule> GetNewModules(List<AppModule> modules, List<ModuleSetting> moduleSettings)
    {
        var newModules = new List<AppModule>();

        foreach (var setting in moduleSettings)
        {
            if (modules.Exists(m => m.Identifier.Equals(setting.Identifier)))
                continue;
            
            newModules.Add(new AppModule
            {
                DisplayName = setting.DisplayName,
                Identifier = setting.Identifier,
            });
        }

        return newModules;
    }
}