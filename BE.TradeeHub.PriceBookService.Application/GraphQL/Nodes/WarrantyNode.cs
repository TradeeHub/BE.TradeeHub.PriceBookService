using BE.TradeeHub.PriceBookService.Application.Extensions;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(WarrantyEntity))]
public static class WarrantyNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, WarrantyEntity>> GetWarrantiesGroupedByIdAsync(
        IReadOnlyList<ObjectId> warrantyIds,
        IMongoCollection<WarrantyEntity> warranties,
        CancellationToken cancellationToken)
    {
        var filter = Builders<WarrantyEntity>.Filter.In(m => m.Id, warrantyIds);
        var warrantyList = await warranties.Find(filter).ToListAsync(cancellationToken);

        return warrantyList.ToLookup(material => material.Id);
    }

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<ObjectId, WarrantyEntity>> GetWarrantiesByIdAsync(
        IReadOnlyList<ObjectId> warrantyIds,
        IMongoCollection<WarrantyEntity> warranties,
        CancellationToken cancellationToken)
    {
        var filter = Builders<WarrantyEntity>.Filter.In(m => m.Id, warrantyIds);
        var warrantyList = await warranties.Find(filter).ToListAsync(cancellationToken);
    
        // Group the materials by each material ID and ensure it's read-only
        var warrantyDictionary = warrantyList.ToDictionary(warranty => warranty.Id, warranty => warranty);
    
        return warrantyDictionary;
    }
    
    [NodeResolver]
    public static async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
}