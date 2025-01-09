using API.Domain.Asset.Dto.SupplySources;
using AutoMapper;

namespace API.Domain.Asset.Mappings;

public class AssetSupplySourceMappingProfile : Profile
{
    public AssetSupplySourceMappingProfile()
    {
        CreateMap<NewAssetSupplySourceDto, AssetSupplySource>();
        CreateMap<AssetSupplySource, AssetSupplySourceDto>();
    }
}