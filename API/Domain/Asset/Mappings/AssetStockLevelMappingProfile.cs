using API.Domain.Asset.Dto.StockLevels;
using AutoMapper;

namespace API.Domain.Asset.Mappings;

public class AssetStockLevelMappingProfile : Profile
{
    public AssetStockLevelMappingProfile()
    {
        CreateMap<AssetStockLevel, AssetStockLevelDto>();
    }
}