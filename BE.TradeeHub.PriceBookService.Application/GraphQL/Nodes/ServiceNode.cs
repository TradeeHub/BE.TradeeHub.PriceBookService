using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[ExtendObjectType(typeof(ServiceEntity), IgnoreProperties = ["UserOwnerId", "CreatedBy", "ModifiedBy", "TaxRateId"])]
public static class ServiceNode
{
    public static async Task<List<WarrantyEntity>> GetWarranties([Parent] ServiceEntity service,
        IWarrantiesGroupedByIdDataLoader warrantyByIdDataLoader, CancellationToken ctx)
    {
        if (service.Warranties == null || service.Warranties.Count == 0)
        {
            return [];
        }

        var warrantyGroups = await warrantyByIdDataLoader.LoadAsync(service.Warranties, ctx);

        var warranties = warrantyGroups.SelectMany(group => group).ToList();

        return warranties;
    }

    public static async Task<List<ServiceBundleEntity>> GetBundles([Parent] ServiceEntity service,
        IServiceBundleGroupedByIdDataLoader serviceBundleGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.Bundles == null || service.Bundles.Count == 0)
        {
            return [];
        }

        var serviceBundleGroups = await serviceBundleGroupedByIdDataLoader.LoadAsync(service.Bundles, ctx);

        var bundles = serviceBundleGroups.SelectMany(group => group).ToList();

        return bundles;
    }

    public static async Task<TaxRateEntity?> GetTaxRate([Parent] ServiceEntity service,
        ITaxRateGroupedByIdDataLoader taxRateByIdDataLoader, CancellationToken ctx)
    {
        if (service.TaxRateId == ObjectId.Empty)
        {
            return null;
        }

        var taxRate = await taxRateByIdDataLoader.LoadAsync(service.TaxRateId, ctx);

        return taxRate.FirstOrDefault();
    }

    public static async Task<ServiceCategoryEntity?> GetServiceCategory([Parent] ServiceEntity service,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.ServiceCategory == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategory = await serviceCategoryGroupedByIdDataLoader.LoadAsync(service.ServiceCategory, ctx);

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