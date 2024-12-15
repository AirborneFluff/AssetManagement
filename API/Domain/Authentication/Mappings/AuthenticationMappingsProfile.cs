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
                opt => opt.MapFrom((src, dest, destMember, context) =>
                    context.Items.TryGetValue("Role", out var role) ? role.ToString() : null
            ));
    }
}