using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ServiceCategoryEntity), IgnoreProperties = ["UserOwnerId", "CreatedBy", "ModifiedBy", "ServiceCategoryIds", "ServiceIds"])]
public static class ServiceCategoryNode
{
    public static async Task<ServiceCategoryEntity?> GetParentServiceCategory([Parent] ServiceCategoryEntity serviceCategory,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (serviceCategory.ParentServiceCategoryId == null || serviceCategory.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategories = await serviceCategoryGroupedByIdDataLoader.LoadAsync(serviceCategory.ParentServiceCategoryId.Value, ctx);

        return serviceCategories.FirstOrDefault();
    }
    
    public static async Task<List<ServiceEntity>> GetServices([Parent] ServiceCategoryEntity serviceCategory,
        IServicesGroupedByIdDataLoader servicesGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (serviceCategory.ServiceIds == null || serviceCategory.ServiceIds.Count == 0)
        {
            return [];
        }

        var serviceGroups = await servicesGroupedByIdDataLoader.LoadAsync(serviceCategory.ServiceIds, ctx);

        var services = serviceGroups.SelectMany(group => group).ToList();

        return services;
    }
    
    public static async Task<List<ServiceCategoryEntity>> GetServiceCategories([Parent] ServiceCategoryEntity serviceCategory,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (serviceCategory.ServiceCategoryIds == null || serviceCategory.ServiceCategoryIds.Count == 0)
        {
            return [];
        }
    
        var serviceCategoryGroups = await serviceCategoryGroupedByIdDataLoader.LoadAsync(serviceCategory.ServiceCategoryIds, ctx);
    
        var serviceCategories = serviceCategoryGroups.SelectMany(group => group).ToList();
    
        return serviceCategories;
    }

    [DataLoader]
    internal static async Task<ILookup<ObjectId, ServiceCategoryEntity>> GetServiceCategoryGroupedByIdAsync(
        IReadOnlyList<ObjectId> serviceCategoriesIds,
        IMongoCollection<ServiceCategoryEntity> serviceCategories,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ServiceCategoryEntity>.Filter.In(m => m.Id, serviceCategoriesIds);
        var serviceCategoryList = await serviceCategories.Find(filter).ToListAsync(cancellationToken);

        return serviceCategoryList.ToLookup(serviceCategory => serviceCategory.Id);
    }
    
    [DataLoader]
    internal static async Task<ServiceCategoryEntity?> GetParentServiceCategoryByIdAsync(
        ObjectId parentServiceCategoryId,
        IMongoCollection<ServiceCategoryEntity> serviceCategories,
        CancellationToken cancellationToken)
    {
        // The filter should directly query for the single ID, not use `In` which is for multiple IDs
        var filter = Builders<ServiceCategoryEntity>.Filter.Eq(m => m.Id, parentServiceCategoryId);
    
        // Since you're expecting a single result, you can use FindOneAsync instead of Find + ToListAsync
        var serviceCategory = await serviceCategories.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return serviceCategory;
    }
}