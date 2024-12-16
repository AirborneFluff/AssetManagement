using API.Domain.Shared.Helpers;
using AutoMapper;

namespace API.Domain.Shared.Mappings;

public class SharedMappingProfile : Profile
{
    public SharedMappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}