using API.Domain.Authentication.Dtos;
using AutoMapper;

namespace API.Domain.Authentication.Mappings;

public class AuthenticationMappingsProfile : Profile
{
    public AuthenticationMappingsProfile()
    {
        CreateMap<AppUser, AppUserDto>()
            .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom((_, _, _, context) =>
                    context.Items.TryGetValue("Role", out var role) ? role.ToString() : null
                ))
            .ForMember(
                dest => dest.ModulesVersion,
                opt => opt.MapFrom((_, _, _, context) =>
                    context.Items.TryGetValue("ModulesVersion", out var mVersion) ? mVersion : null
                ))
            .ForMember(
                dest => dest.Modules,
                opt => opt.MapFrom((_, _, _, context) =>
                    context.Items.TryGetValue("Modules", out var modules) ? modules : null
                ));
    }
}