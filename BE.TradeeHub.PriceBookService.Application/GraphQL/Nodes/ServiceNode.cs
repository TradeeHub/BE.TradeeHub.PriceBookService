using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ServiceEntity), IgnoreProperties = ["UserOwnerId", "CreatedBy", "ModifiedBy", "TaxRateId", "WarrantyIds", "BundleIds", "ServiceCategoryId"])]
public static class ServiceNode
{
    public static async Task<List<WarrantyEntity>> GetWarranties([Parent] ServiceEntity service,
        IWarrantiesGroupedByIdDataLoader warrantyByIdDataLoader, CancellationToken ctx)
    {
        if (service.WarrantyIds == null || service.WarrantyIds.Count == 0)
        {
            return [];
        }

        var warrantyGroups = await warrantyByIdDataLoader.LoadAsync(service.WarrantyIds, ctx);

        var warranties = warrantyGroups.SelectMany(group => group).ToList();

        return warranties;
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
        if (service.ServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategory = await serviceCategoryGroupedByIdDataLoader.LoadAsync(service.ServiceCategoryId, ctx);

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