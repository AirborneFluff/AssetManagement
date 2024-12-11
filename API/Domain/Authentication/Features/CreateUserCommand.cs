using API.Data;
using API.Domain.Authentication.Dtos;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Authentication.Features;

public record CreateUserCommand(NewAppUserDto AppUserDto, string TenantId) : IRequest<Result<AppUserDto>>;

public class CreateUserCommandHandler(UnitOfWork unitOfWork, 
    UserManager<AppUser> userManager, 
    IMapper mapper) 
    : IRequestHandler<CreateUserCommand, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Context.Tenants
            .SingleOrDefaultAsync(t => t.Id == request.TenantId, cancellationToken);
        if (tenant is null) return Result.Fail($"Tenant {request.TenantId} does not exist.");
        
        var user = new AppUser(request.AppUserDto.Email, request.TenantId);
        var result = await userManager.CreateAsync(user, request.AppUserDto.Password);

        if (!result.Succeeded) return Result.Fail(result.Errors.Select(e => e.Description));
        return mapper.Map<AppUserDto>(user);
    }
}