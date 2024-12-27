using API.Data.Interfaces;
using API.Domain.Asset.Dto;
using API.Domain.Asset.Params;
using API.Domain.Shared.Helpers;
using API.Extensions;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record GetAssetsCommand(GetAssetsParams PageParams) : IRequest<Result<PagedList<AssetDto>>>;

public class GetAssetsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAssetsCommand, Result<PagedList<AssetDto>>>
{
    public async Task<Result<PagedList<AssetDto>>> Handle(GetAssetsCommand request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Context.Assets
            .AsNoTracking()
            .AsQueryable()
            .Include(a => a.Tags)
            .WhereContains(x => x.Description, request.PageParams.Description)
            .OrderByField(request.PageParams.SortField, request.PageParams.SortOrder);

        var results = await PagedList<Asset>
            .CreateAsync(query, request.PageParams);
        
        return mapper.Map<PagedList<AssetDto>>(results);
    }
}