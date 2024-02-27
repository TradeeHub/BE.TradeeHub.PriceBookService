using BE.TradeeHub.PriceBookService.Application.Extensions;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;

[QueryType]
public static class Query
{
    [Authorize]
    [UseProjection]
    public static async Task<IList<ServiceCategoryEntity>> GetServiceCategories(
        [Service] IPriceBookService priceBookService, [Service] UserContext userContext, CancellationToken ctx)
    {
        // var objectType = resolverContext.Schema.GetType<ObjectType>(nameof(ServiceCategoryEntity));
        //
        // var selections = resolverContext.GetSelections(objectType, null, false);

        return await priceBookService.GetAllServiceCategoriesAsync(userContext, ctx);
    }
    
    [NodeResolver]
    public static async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection, [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [NodeResolver]
    public static async Task<ServiceCategoryEntity?> GetServiceCategory(
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
    public static async Task<WarrantyEntity?> GetWarranty([Service] IMongoCollection<WarrantyEntity?> collection,
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
}