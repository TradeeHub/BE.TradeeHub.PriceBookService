using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ServiceEntity), IgnoreProperties = ["UserOwnerId", "CreatedById", "ModifiedById", "TaxRateId", "WarrantyIds", "BundleIds", "ServiceCategoryId"])]
public static class ServiceNode
{
    public static async Task<ServiceCategoryEntity?> GetServiceCategory([Parent] MaterialEntity material,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (material.ParentServiceCategoryId == null || material.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategories = await serviceCategoryGroupedByIdDataLoader.LoadAsync(material.ParentServiceCategoryId.Value, ctx);

        return serviceCategories.FirstOrDefault();
    }

    public static async Task<List<ServiceBundleEntity>> GetBundles([Parent] ServiceEntity service,
        IServiceBundleGroupedByIdDataLoader serviceBundleGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.BundleIds == null || service.BundleIds.Count == 0)
        {
            return [];
        }

        var serviceBundleGroups = await serviceBundleGroupedByIdDataLoader.LoadAsync(service.BundleIds, ctx);

        var bundles = serviceBundleGroups.SelectMany(group => group).ToList();

        return bundles;
    }

    public static async Task<TaxRateEntity?> GetTaxRate([Parent] ServiceEntity service,
        ITaxRateGroupedByIdDataLoader taxRateByIdDataLoader, CancellationToken ctx)
    {
        if (service.TaxRateId == null || service.TaxRateId == ObjectId.Empty)
        {
            return null;
        }

        var taxRate = await taxRateByIdDataLoader.LoadAsync(service.TaxRateId.Value, ctx);

        return taxRate.FirstOrDefault();
    }

    public static async Task<ServiceCategoryEntity?> GetServiceCategory([Parent] ServiceEntity service,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.ParentServiceCategoryId == null || service.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategory = await serviceCategoryGroupedByIdDataLoader.LoadAsync(service.ParentServiceCategoryId.Value, ctx);

        return serviceCategory.FirstOrDefault();
    }

    [DataLoader]
    internal static async Task<ILookup<ObjectId, ServiceEntity>> GetServicesGroupedByIdAsync(
        IReadOnlyList<ObjectId> servicesIds,
        IMongoCollection<ServiceEntity> services,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ServiceEntity>.Filter.In(m => m.Id, servicesIds);
        var serviceList = await services.Find(filter).ToListAsync(cancellationToken);

        return serviceList.ToLookup(x => x.Id);
    }
}