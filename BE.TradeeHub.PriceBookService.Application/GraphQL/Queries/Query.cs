using BE.TradeeHub.PriceBookService.Application.Extensions;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;

[QueryType]
public class Query
{
    public static async Task<IExecutable<ServiceCategoryEntity>> GetServiceCategories(
        [Service] IMongoCollection<ServiceCategoryEntity> collection, CancellationToken cancellationToken)
    {
        return collection.AsExecutable();
    }

    [NodeResolver]
    public async Task<ServiceCategoryEntity?> GetServiceCategory(
        [Service] IMongoCollection<ServiceCategoryEntity> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);

    }

    [NodeResolver]
    public static async Task<LaborRateEntity?> GetLaborRate([Service] IMongoCollection<LaborRateEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<ServiceBundleEntity?> GetServiceBundle([Service] IMongoCollection<ServiceBundleEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<MaterialEntity?> GetMaterial([Service] IMongoCollection<MaterialEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<TaxRateEntity?> GetTaxRate([Service] IMongoCollection<TaxRateEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<WarrantyEntity?> GetWarranty([Service] IMongoCollection<WarrantyEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
}