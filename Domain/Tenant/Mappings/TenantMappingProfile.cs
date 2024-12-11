using API.Domain.Tenant.DTOs;
using AutoMapper;

namespace API.Domain.Tenant.Mappings;

public class TenantMappingProfile : Profile
{
    public TenantMappingProfile()
    {
        CreateMap<NewTenantDto, AppTenant>();
        CreateMap<AppTenant, TenantDto>();
    }
}