using API.Domain.Asset.Dto;
using API.Domain.Asset.Dto.Suppliers;
using AutoMapper;

namespace API.Domain.Asset.Mappings;

public class AssetSupplierMappingProfile : Profile
{
    public AssetSupplierMappingProfile()
    {
        CreateMap<NewAssetSupplierDto, AssetSupplier>();
        CreateMap<AssetSupplier, AssetSupplierDto>();
    }
}