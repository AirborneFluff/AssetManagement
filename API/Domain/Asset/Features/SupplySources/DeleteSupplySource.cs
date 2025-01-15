using API.Data.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Asset.Features.SupplySources;

public record DeleteSupplySourceCommand(string SupplySourceId) : IRequest<Result>;

public class DeleteSupplySourceHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<DeleteSupplySourceCommand, Result>
{
    public async Task<Result> Handle(DeleteSupplySourceCommand request, CancellationToken cancellationToken)
    {
        var supplySource = await unitOfWork.Context.AssetSupplySources
            .FirstOrDefaultAsync(t => t.Id == request.SupplySourceId, cancellationToken);
        if (supplySource is null) return Result.Fail("Supply Source not found");
        
        unitOfWork.Context.AssetSupplySources.Remove(supplySource);
        
        if (!await unitOfWork.Complete()) return Result.Fail("There was an issue deleting supply source.");
        return Result.Ok();
    }
}