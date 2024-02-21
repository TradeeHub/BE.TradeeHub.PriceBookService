using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;

public class Query
{
    public static async Task<IExecutable<ServiceCategoryEntity>> GetServiceCategories([Service] IMongoCollection<ServiceCategoryEntity> collection, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations if needed
        return collection.AsExecutable();
    }
    
    [NodeResolver]
    public async Task<ServiceCategoryEntity?> GetServiceCategory([Service] IMongoCollection<ServiceCategoryEntity> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        var idFilter = Builders<ServiceCategoryEntity>.Filter.Eq(x => x.Id, id);
        var ownerFilter = Builders<ServiceCategoryEntity>.Filter.Eq(x => x.UserOwnerId, userContext.UserId);
        var combinedFilter = Builders<ServiceCategoryEntity>.Filter.And(ownerFilter, idFilter);
        return await collection.Find(combinedFilter).FirstOrDefaultAsync(ctx);
    }
}