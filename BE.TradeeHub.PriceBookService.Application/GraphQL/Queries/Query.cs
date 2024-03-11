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
    [UsePaging(MaxPageSize = 100)]
    [UseSorting]
    [UseFiltering]
    public static IExecutable<ServiceCategoryEntity> GetServiceCategories(
        [Service] IMongoCollection<ServiceCategoryEntity> collection,
        [Service] UserContext userContext,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = collection.Find(x => x.UserOwnerId == userContext.UserId);
            var executableQuery = query.AsExecutable();

            return executableQuery;
        }
        catch (Exception e)
        {
            throw new Exception("Error while fetching service categories", e);
        }
    }
    
    // [Authorize]
    // [UseProjection]
    // public static async Task<IList<ServiceCategoryEntity>> GetServiceCategories(
    //     [Service] IPriceBookService priceBookService, [Service] UserContext userContext, IResolverContext context,
    //     CancellationToken ctx)
    // {
    //     var projection = context.Selection.DeclaringSelectionSet.Selections.ToBsonDocumentProjection();
    //     return await priceBookService.GetAllServiceCategoriesAsync(userContext, projection, ctx);
    // }

    [NodeResolver]
    public static async Task<ServiceEntity?> GetService([Service] IMongoCollection<ServiceEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
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
    public static async Task<ServiceBundleEntity?> GetServiceBundle(
        [Service] IMongoCollection<ServiceBundleEntity?> collection,
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