using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Requests.Update;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IMaterialRepository
{
    Task<(OperationResult, MaterialEntity?)> DeleteMaterialAsync(ObjectId id, CancellationToken ctx);
    Task<MaterialEntity?> UpdateMaterialAsync(IUserContext userContext, UpdateMaterialRequest request, OperationResult operationResult, CancellationToken ctx, ImageEntity? newImage = null);
    Task<MaterialEntity> CreateMaterialAsync(MaterialEntity material, CancellationToken ctx);
}