using BE.TradeeHub.PriceBookService.Application.Extensions;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Authorization;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;

[QueryType]
public static class Query
{
    [Authorize]
    [UsePaging(MaxPageSize = 1000)]
    [UseProjection]
    [HotChocolate.Types.UseSorting]
    [HotChocolate.Types.UseFiltering]
    public static async Task<IExecutable<ServiceCategoryEntity>> GetServiceCategories(
        [Service] IMongoCollection<ServiceCategoryEntity> collection, CancellationToken cancellationToken)
    {
        return collection.AsExecutable();
    }
    
    [NodeResolver]
    public static async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    // [NodeResolver]
    // public async Task<ServiceCategoryEntity?> GetServiceCategory(
    //     [Service] IMongoCollection<ServiceCategoryEntity> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }
    //
    // [NodeResolver]
    // public async Task<LaborRateEntity?> GetLaborRate([Service] IMongoCollection<LaborRateEntity?> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }
    //
    // [NodeResolver]
    // public async Task<ServiceBundleEntity?> GetServiceBundle([Service] IMongoCollection<ServiceBundleEntity?> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }
    //
    // [NodeResolver]
    // public async Task<MaterialEntity?> GetMaterial([Service] IMongoCollection<MaterialEntity?> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }
    //
   
    //
    // [NodeResolver]
    // public async Task<WarrantyEntity?> GetWarranty([Service] IMongoCollection<WarrantyEntity?> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }
    //    
    // [NodeResolver]
    // public async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection,
    //     [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    // {
    //     return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    // }

}