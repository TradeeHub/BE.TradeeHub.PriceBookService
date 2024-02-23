using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.Extensions;

internal static class EntityFetcher
{
    public static async Task<T?> GetEntityByIdAndOwnerId<T>(
        IMongoCollection<T> collection,
        ObjectId id,
        Guid userOwnerId,
        CancellationToken cancellationToken) where T : IOwnedEntity?
    {
        var idFilter = Builders<T>.Filter.Eq(entity => entity!.Id, id);
        var ownerFilter = Builders<T>.Filter.Eq(entity => entity!.UserOwnerId, userOwnerId);
        var combinedFilter = Builders<T>.Filter.And(idFilter, ownerFilter);

        return await collection.Find(combinedFilter).FirstOrDefaultAsync(cancellationToken);
    }
}