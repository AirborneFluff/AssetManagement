using API.Data.Interfaces;
using API.Domain.Asset.Dto.Categories;
using API.Domain.Asset.Params;
using API.Domain.Shared.Helpers;
using API.Extensions;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.Categories;

public record GetAssetCategoriesCommand(GetAssetCategoriesParams PageParams) : IRequest<Result<PagedList<AssetCategoryDto>>>;

public class GetAssetCategoriesHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAssetCategoriesCommand, Result<PagedList<AssetCategoryDto>>>
{
    public async Task<Result<PagedList<AssetCategoryDto>>> Handle(GetAssetCategoriesCommand request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Context.AssetCategories
            .AsNoTracking()
            .AsQueryable()
            .WhereContains(x => x.Name, request.PageParams.Name)
            .OrderByField(request.PageParams.SortField, request.PageParams.SortOrder);

        var results = await PagedList<AssetCategory>
            .CreateAsync(query, request.PageParams);
        
        return mapper.Map<PagedList<AssetCategoryDto>>(results);
    }
}