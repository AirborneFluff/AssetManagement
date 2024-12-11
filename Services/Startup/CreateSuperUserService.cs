using API.Domain.Authentication;
using API.Domain.Authentication.Constants;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Startup;

public class CreateSuperUserService(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration config) : IStartupService
{
    public async Task<Result> Execute()
    {
        if (await userManager.Users.AnyAsync()) return Result.Ok();

        var email = config.GetValue<string>("SuperUser:Email");
        var password = config.GetValue<string>("SuperUser:Password");
        if (email is null || password is null) return Result.Fail("Superuser hasn't been configured");

        var superUser = new AppUser(email);
        var userResult = await userManager.CreateAsync(superUser, password);
        if (!userResult.Succeeded) return Result.Fail(userResult.Errors.Select(e => e.Description));

        if (!await roleManager.RoleExistsAsync(AppUserRoles.SuperUser))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(AppUserRoles.SuperUser));
            if (!roleResult.Succeeded) return Result.Fail(roleResult.Errors.Select(e => e.Description));
        }
        
        var addRoleResult = await userManager.AddToRoleAsync(superUser, AppUserRoles.SuperUser);
        if (addRoleResult.Succeeded) return Result.Ok();
        
        return Result.Fail(addRoleResult.Errors.Select(e => e.Description));
    }
}