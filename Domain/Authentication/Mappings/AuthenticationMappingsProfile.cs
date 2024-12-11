using API.Domain.Authentication.Dtos;
using AutoMapper;

namespace API.Domain.Authentication.Mappings;

public class AuthenticationMappingsProfile : Profile
{
    public AuthenticationMappingsProfile()
    {
        CreateMap<AppUser, AppUserDto>();
    }
}