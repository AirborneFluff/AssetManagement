using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppUserRoles = API.Domain.Authentication.Constants.AppUserRoles;

namespace API.Services.Startup;

public class SeedUserRolesService(RoleManager<IdentityRole> roleManager) : IStartupService
{
    public async Task<Result> Execute()
    {
        var roles = AppUserRoles.GetRoles();

        var rolesToCreate = (await Task.WhenAll(
                roles.Select(async role => new
                {
                    Role = role,
                    Exists = await roleManager.RoleExistsAsync(role)
                })
            ))
            .Where(result => !result.Exists)
            .Select(result => result.Role);

        var roleTasks = rolesToCreate
            .Select(role => roleManager.CreateAsync(new IdentityRole(role)));

        var results = await Task.WhenAll(roleTasks);
        if (results.All(r => r.Succeeded)) return Result.Ok();
        return Result.Fail(results.SelectMany(r => r.Errors).Select(e => e.Description));
    }
}