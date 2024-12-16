using API.Domain.Asset.Dto;
using AutoMapper;

namespace API.Domain.Asset.Mappings;

public class AssetMappingProfile : Profile
{
    public AssetMappingProfile()
    {
        CreateMap<NewAssetDto, Asset>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore());
            
        CreateMap<Asset, AssetDto>();
        CreateMap<AssetTag, AssetTagDto>();
    }
}