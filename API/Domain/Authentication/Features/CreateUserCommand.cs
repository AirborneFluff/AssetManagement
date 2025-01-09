using API.Data.Interfaces;
using API.Domain.Authentication.Dtos;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Authentication.Features;

public record CreateUserCommand(NewAppUserDto AppUserDto, string TenantId) : IRequest<Result<AppUserDto>>;

public class CreateUserCommandHandler(
    IUnitOfWork unitOfWork, 
    UserManager<AppUser> userManager, 
    RoleManager<IdentityRole> roleManager,
    IMapper mapper) 
    : IRequestHandler<CreateUserCommand, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Context.Tenants
            .SingleOrDefaultAsync(t => t.Id == request.TenantId, cancellationToken);
        if (tenant is null) return Result.Fail($"Tenant {request.TenantId} does not exist.");
        
        var role = await roleManager.FindByNameAsync(request.AppUserDto.Role);
        if (role is null) return Result.Fail($"Role {request.AppUserDto.Role} does not exist.");
        
        var user = new AppUser(request.AppUserDto.Email, request.TenantId);
        var createUserResult = await userManager.CreateAsync(user, request.AppUserDto.Password);
        if (!createUserResult.Succeeded) return Result.Fail(createUserResult.Errors.Select(e => e.Description));
        
        var addRoleResult = await userManager.AddToRoleAsync(user, role.Name!);
        if (!addRoleResult.Succeeded) return Result.Fail(addRoleResult.Errors.Select(e => e.Description));
    
        var userDto = mapper.Map<AppUserDto>(user, opt =>
        {
            opt.Items["Role"] = role;
        });
        return userDto;
    }
}