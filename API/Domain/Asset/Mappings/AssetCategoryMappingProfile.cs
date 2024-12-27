using API.Domain.Asset.Dto;
using AutoMapper;

namespace API.Domain.Asset.Mappings;

public class AssetCategoryMappingProfile : Profile
{
    public AssetCategoryMappingProfile()
    {
        CreateMap<NewAssetCategoryDto, AssetCategory>();
        CreateMap<AssetCategory, AssetCategoryDto>();
    }
}