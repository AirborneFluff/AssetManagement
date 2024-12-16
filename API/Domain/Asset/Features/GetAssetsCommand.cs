using API.Data;
using API.Domain.Asset.Dto;
using API.Domain.Shared;
using API.Domain.Shared.Helpers;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features;

public record GetAssetsCommand(BasePaginationParams PageParams) : IRequest<Result<PagedList<AssetDto>>>;

public class GetAssetsHandler(
    UnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAssetsCommand, Result<PagedList<AssetDto>>>
{
    public async Task<Result<PagedList<AssetDto>>> Handle(GetAssetsCommand request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Context.Assets
            .AsNoTracking()
            .AsQueryable()
            .Include(a => a.Tags);
        
        var results = await PagedList<Asset>
            .CreateAsync(query, request.PageParams);
        
        return mapper.Map<PagedList<AssetDto>>(results);
    }
}